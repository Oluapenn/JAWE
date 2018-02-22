using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Network.Messaging.Validators
{
    public class Parameter : IParameter
    {
        private readonly AbstractMessageAttribute[] _attributes;
        private readonly TypeConverter _converter;

        private readonly MemberInfo _memberInfo;

        public Parameter(MemberInfo memberInfo, IEnumerable<AbstractMessageAttribute> attributes)
        {
            _memberInfo = memberInfo;
            _attributes = attributes.ToArray();

            ParameterType = GetParameterType(memberInfo);
            if (ParameterType.IsArray)
            {
                ParameterType = ParameterType.GetElementType();
                IsArray = true;
            }

            IsMessage = ParameterType.GetInterfaces().Contains(typeof(IMessage));

            _converter = TypeDescriptor.GetConverter(ParameterType != typeof(bool)
                ? ParameterType
                : typeof(byte)
            );
        }

        public bool IsArray { get; }
        public bool IsMessage { get; }
        public Type ParameterType { get; }

        public bool HasAttribute<T>() where T : AbstractMessageAttribute
        {
            return GetAttribute<T>() != null;
        }

        public T GetAttribute<T>() where T : AbstractMessageAttribute
        {
            return (T)_attributes.FirstOrDefault(x => x.GetType() == typeof(T));
        }

        public ParameterValidationResult Validate(/*MessageProcessor processor, */IMessage target, string parameter)
        {
            if (HasAttribute<ParameterFromAttribute>())
            {
                return new ParameterValidationResult(ParameterValidationStatus.TakeFromTargetField,
                    GetAttribute<ParameterFromAttribute>().Index);
            }

            if (IsArray)
            {
                // tell message validator to parse an array with leading count parameter
                return new ParameterValidationResult(ParameterValidationStatus.Array, 0);
            }

            if (IsMessage)
            {
                // tell message validator to validate nested type
                return new ParameterValidationResult(ParameterValidationStatus.Nested, 0);
            }

            object convertedValue;
            try
            {
                convertedValue = Convert.ChangeType(_converter.ConvertFromString(parameter), ParameterType);
            }
            catch (Exception)
            {
                throw new MessageProcessorException("Could not convert value");
            }

            if (HasAttribute<ExactAttribute>())
            {
                if (!convertedValue.Equals(GetAttribute<ExactAttribute>().ExactMatch))
                {
                    throw new MessageProcessorException("Exact failed");
                }
            }

            if (ParameterType == typeof(string))
            {
                var minLengthAttribute = GetAttribute<MinLengthAttribute>();
                var maxLengthAttribute = GetAttribute<MaxLengthAttribute>();
                convertedValue = ((string)convertedValue).Replace('\x1D', '\x20');

                if (minLengthAttribute != null && ((string)convertedValue).Length < minLengthAttribute.MinLength)
                {
                    throw new MessageProcessorException("MinLength failed");
                }

                if (maxLengthAttribute != null && ((string)convertedValue).Length > maxLengthAttribute.MaxLength)
                {
                    throw new MessageProcessorException("MaxLength failed");
                }
            }

            if (HasAttribute<MatchAttribute>())
            {
                var matchAttribute = GetAttribute<MatchAttribute>();
                var match = Regex.Match(convertedValue.ToString(), matchAttribute.Expression);
                if (!match.Success)
                {
                    throw new MessageProcessorException("Match failed");
                }

                if (!string.IsNullOrEmpty(matchAttribute.ExtractGroup))
                {
                    convertedValue = match.Groups[matchAttribute.ExtractGroup].Value;
                }
            }

            if (HasAttribute<RangeAttribute>())
            {
                var rangeAttribute = GetAttribute<RangeAttribute>();
                var decimalConvertedValue = Convert.ToDecimal(convertedValue);
                if (decimalConvertedValue < rangeAttribute.Minimum ||
                    decimalConvertedValue > rangeAttribute.Maximum)
                {
                    throw new MessageProcessorException("Range failed");
                }
            }

            SetValueOnTarget(target, convertedValue);
            return new ParameterValidationResult(ParameterValidationStatus.Success);
        }

        private Type GetParameterType(MemberInfo memberInfo)
        {
            Type parameterType = null;

            if (memberInfo is PropertyInfo)
            {
                parameterType = ((PropertyInfo)memberInfo).PropertyType;
            }
            else if (memberInfo is FieldInfo)
            {
                parameterType = ((FieldInfo)memberInfo).FieldType;
            }

            if (parameterType == null)
            {
                throw new Exception("This should never happen");
            }

            return Nullable.GetUnderlyingType(parameterType) ?? parameterType;
        }

        public void SetValueOnTarget<T>(IMessage target, T value)
        {
            if (_memberInfo is PropertyInfo)
            {
                ((PropertyInfo)_memberInfo).SetValue(target, value);
            }
            else if (_memberInfo is FieldInfo)
            {
                ((FieldInfo)_memberInfo).SetValue(target, value);
            }
        }

        public object GetValueFromTarget(IMessage target)
        {
            object returnValue = null;
            if (_memberInfo is PropertyInfo)
            {
                returnValue = ((PropertyInfo)_memberInfo).GetValue(target);
            }
            else if (_memberInfo is FieldInfo)
            {
                returnValue = ((FieldInfo)_memberInfo).GetValue(target);
            }
            else
            {
                throw new MessageProcessorException("Member is not a property or field");
            }

            var typeInfo = returnValue.GetType().GetTypeInfo();

            if (returnValue != null && typeInfo.IsEnum)
            {
                return Convert.ChangeType(returnValue, typeInfo.GetEnumUnderlyingType());
            }

            return returnValue;
        }
    }
}