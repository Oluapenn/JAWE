using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Network.Messaging.Validators
{
    public class MessageValidator
    {
        public Type MessageType { get; }
        public Parameter[] Parameters { get; }

        public MessageValidator(Type messageType)
        {
            MessageType = messageType;

            var parameters = new List<Parameter>();

            parameters.AddRange(ExtractParameters(messageType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)));
            parameters.AddRange(ExtractParameters(messageType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)));

            Parameters = (from Parameter param in parameters let index = param.GetAttribute<ParameterAttribute>().Index orderby index select param).ToArray();
        }

        /// <summary>
        /// Validate from parameters. Returns null if something went wrong
        /// </summary>
        /// <returns>Instance of MessageType or null if something went wrong</returns>
        public MessageValidationResult Validate(MessageProcessor messageProcessor, string[] parameters, int offset = 0)
        {
            var parameterOffset = offset;
            var message = (IMessage)MessageType.GetConstructor(new Type[0]).Invoke(new object[0]);

            for (var i = 0; i < Parameters.Length; i++)
            {
                // if we fail here at some point to validate a parameter: return null immediately
                var param = Parameters[i];
                var required = param.GetAttribute<ParameterAttribute>().Required;

                if (i + parameterOffset < parameters.Length)
                {
                    try
                    {
                        var currentParamValue = parameters[i + parameterOffset];

                        if (string.IsNullOrEmpty(currentParamValue))
                        {
                            if (required)
                            {
                                throw new MessageProcessorException("Not enough parameters");
                            }

                            break; // we're done
                        }

                        var result = param.Validate(message, currentParamValue);

                        switch (result.Status)
                        {
                            case ParameterValidationStatus.Success:
                                parameterOffset += result.ParametersTaken - 1;
                                break;

                            case ParameterValidationStatus.TakeFromTargetField:
                                param.SetValueOnTarget(message, Parameters[result.ParametersTaken].GetValueFromTarget(message));
                                break;

                            case ParameterValidationStatus.Nested:
                                var nestedValidationResult = messageProcessor.MatchNestedType(param.ParameterType, parameters, i + parameterOffset);

                                parameterOffset += nestedValidationResult.ParametersTaken - 1;
                                param.SetValueOnTarget(message, nestedValidationResult.Message);
                                break;

                            case ParameterValidationStatus.Array:
                                //var countParameterInformation = messageProcessor.CountParameter.Validate(null, currentParamValue);
                                parameterOffset++;

                                /*var count = (int)countParameterInformation.State;
                                if (count > parameters.Length)
                                {
                                    throw new MessageProcessorException(
                                        string.Format("Not enough parameters for array count (count = {0}, _parameters.Length = {1})", count, parameters.Length)
                                    );
                                }

                                if (count < 0)
                                {
                                    throw new MessageProcessorException("Negative array count");
                                }

                                var parameterArray = Array.CreateInstance(param.ParameterType, count);
                                for (var j = 0; j < count; j++)
                                {
                                    var arrayValidationResult = messageProcessor.MatchNestedType(param.ParameterType, parameters, i + parameterOffset);
                                    parameterOffset += arrayValidationResult.ParametersTaken;
                                    parameterArray.SetValue(arrayValidationResult.Message, j);
                                }*/

                                parameterOffset--;
                                //param.SetValueOnTarget(message, parameterArray);
                                break;

                            default:
                                throw new MessageProcessorException(string.Format("Could not validate parameter (index = {0})", i));
                        }

                    }
                    catch (MessageProcessorException parserException)
                    {
                        throw new MessageProcessorException(
                            string.Format("Could not validate parameter (index = {0})", i), parserException);
                    }
                }
                else if (!required)
                {
                    break; // no required parameters anymore
                }
                else
                {
                    throw new MessageProcessorException("Not enough parameters");
                }
            }
            return new MessageValidationResult(message, Parameters.Length + parameterOffset - offset);
        }

        private static IEnumerable<Parameter> ExtractParameters(IEnumerable<MemberInfo> member)
        {
            return member.Select(info => new Parameter(info, info.GetCustomAttributes<AbstractMessageAttribute>(true)));
        }
    }
}
