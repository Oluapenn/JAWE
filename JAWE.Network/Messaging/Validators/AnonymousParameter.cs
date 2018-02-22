using System;
using System.ComponentModel;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Network.Messaging.Validators
{
    public class AnonymousParameter : IParameter
    {
        private readonly TypeConverter _converter;

        public AnonymousParameter(Type parameterType)
        {
            ParameterType = parameterType;
            _converter = TypeDescriptor.GetConverter(ParameterType);
        }

        public Type ParameterType { get; }

        public T GetAttribute<T>() where T : AbstractMessageAttribute
        {
            return null;
        }

        public bool HasAttribute<T>() where T : AbstractMessageAttribute
        {
            return GetAttribute<T>() != null;
        }

        public ParameterValidationResult Validate(/*MessageProcessor processor, */IMessage target, string parameter)
        {
            object convertedValue;
            try
            {
                convertedValue = _converter.ConvertFromString(parameter);
            }
            catch (Exception)
            {
                throw new MessageProcessorException("Could not convert value");
            }
            return new ParameterValidationResult(ParameterValidationStatus.Success, 1) {State = convertedValue};
        }
    }
}