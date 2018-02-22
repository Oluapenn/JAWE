using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JAWE.Network.Messages;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Resolving;
using JAWE.Network.Messaging.Validators;

namespace JAWE.Network.Messaging.Processing
{

    public class MessageProcessor
    {
        // Temporary
        private static readonly WrMessageResolver MessageResolver = new WrMessageResolver();

        #region Static

        private static readonly Dictionary<Type, IParameter> PrimitiveValidators;
        private static readonly MessageValidator AbstractMessageValidator;

        static MessageProcessor()
        {
            PrimitiveValidators = new Dictionary<Type, IParameter>
            {
                {typeof(string), new AnonymousParameter(typeof(string))},
                {typeof(byte), new AnonymousParameter(typeof(byte))},
                {typeof(sbyte), new AnonymousParameter(typeof(sbyte))},
                {typeof(ushort), new AnonymousParameter(typeof(ushort))},
                {typeof(short), new AnonymousParameter(typeof(short))},
                {typeof(uint), new AnonymousParameter(typeof(uint))},
                {typeof(int), new AnonymousParameter(typeof(int))},
                {typeof(ulong), new AnonymousParameter(typeof(ulong))},
                {typeof(long), new AnonymousParameter(typeof(long))}
            };

            AbstractMessageValidator = new MessageValidator(typeof(BaseMessage));
        }

        #endregion

        private readonly Dictionary<MessageId, Type> _messages;
        private readonly Dictionary<Type, MessageAttribute> _messageAttributes;
        private readonly Dictionary<Type, MessageValidator> _nestedValidators;
        private readonly Dictionary<Type, MessageValidator> _validators;
        internal IParameter CountParameter;

        public int MessagesCount => _messages.Count;

        public MessageProcessor()
        {
            _messages = new Dictionary<MessageId, Type>();
            _messageAttributes = new Dictionary<Type, MessageAttribute>();

            _validators = new Dictionary<Type, MessageValidator>();
            _nestedValidators = new Dictionary<Type, MessageValidator>();

            CountParameter = new AnonymousParameter(typeof(int));
            
            // All all the messages to the cache.
            AddMessages(MessageResolver.Resolve());
        }

        /// <summary>
        /// Add messages to match / process
        /// </summary>
        private void AddMessages(IEnumerable<ResolvedMessagesSet> messages)
        {
            foreach (var resultSet in messages)
            {
                var messageAttribute = resultSet.MessageAttribute;
                if (messageAttribute == null)
                    continue;

                var serverMessage = messageAttribute.Server;
                if (!serverMessage)
                {
                    _messages.Add((MessageId)messageAttribute.Id, resultSet.Type);
                }

                _messageAttributes.Add(resultSet.Type, messageAttribute);

                var validator = new MessageValidator(resultSet.Type);
                _validators.Add(resultSet.Type, validator);

                foreach (var parameter in validator.Parameters)
                {
                    // is nested parameter
                    if (!_nestedValidators.ContainsKey(parameter.ParameterType) && parameter.ParameterType.GetInterfaces().Contains(typeof(IMessage)))
                    {
                        _nestedValidators.Add(parameter.ParameterType, new MessageValidator(parameter.ParameterType));
                    }
                }
            }
        }
        
        /// <summary>
        /// Parse a warrock packet and match it
        /// </summary>
        /// <returns>Null if something went wrong</returns>
        public IMessage Parse(string wrMessage)
        {
            var parameters = wrMessage.Split(' ');
            if (parameters.Length < 2)
            {
                throw new MessageProcessorException("Packet requires at least 2 parameters");
            }

            Console.WriteLine("Parsing: {0}", wrMessage);

            try
            {
                var matchedMessage = ParseFromParameters(parameters);
                var message = matchedMessage as IMessageSerialization;
                message?.Deserialized();
                return matchedMessage;
            }
            catch (MessageProcessorException exception)
            {
                throw new MessageProcessorException("Could not parse and validate warrock packet", exception);
            }
        }

        private IMessage ParseFromParameters(string[] parameters)
        {
            var abstractMessage = AbstractMessageValidator.Validate(this, parameters).Message as BaseMessage;

            if (abstractMessage == null)
            {
                throw new MessageProcessorException("Could not validate AbstractMessage");
            }

            if (!Enum.IsDefined(typeof(MessageId), abstractMessage.Id))
            {
                throw new MessageProcessorException(
                    string.Format("MessageId not defined (messageId = {0})", abstractMessage.Id));
            }

            if (!_messages.ContainsKey(abstractMessage.Id))
            {
                throw new MessageProcessorException(
                    string.Format("Message not registered (messageId = {0})", abstractMessage.Id));
            }

            if (!_validators.ContainsKey(_messages[abstractMessage.Id]))
            {
                throw new MessageProcessorException(
                    string.Format("Validator not found (messageId = {0})", abstractMessage.Id));
            }

            return Match(abstractMessage, abstractMessage.Id, parameters.Skip(2).ToArray()).Message as IMessage;
        }

        /// <summary>
        /// Match an AbstractMessage with a MessageValidator from parameters
        /// </summary>
        /// <returns>Null if something went wrong</returns>
        private MessageValidationResult Match(BaseMessage abstractMessage, MessageId id, string[] parameters)
        {
            var validator = _validators[_messages[id]];
            var result = validator.Validate(this, parameters);

            foreach (var parameter in AbstractMessageValidator.Parameters)
            {
                parameter.SetValueOnTarget((IMessage)result.Message, parameter.GetValueFromTarget(abstractMessage));
            }

            return result;
        }

        internal MessageValidationResult MatchNestedType(Type nestedType, string[] parameters, int offset)
        {
            if (PrimitiveValidators.ContainsKey(nestedType))
            {
                var result = PrimitiveValidators[nestedType].Validate(null, parameters[offset]);
                return new MessageValidationResult(result.State, 1);
            }

            var validator = _nestedValidators[nestedType];

            return validator.Validate(this, parameters, offset);
        }

        /// <summary>
        /// Converts an AbstractMessage to string
        /// </summary>
        public string Build(BaseMessage message)
        {
            var messageType = message.GetType();
            if (!_messageAttributes.ContainsKey(messageType))
            {
                throw new MessageProcessorException("Message Type not found in the MessageAttributes cache");
            }

            var messageAttribute = _messageAttributes[messageType];
            if (messageAttribute == null)
            {
                throw new MessageProcessorException("Could not find MessageAttribute on AbstractMessage");
            }

            var parametersToTake = -1;
            var serialization = message as IMessageSerialization;
            if (serialization != null)
            {
                parametersToTake = serialization.BeforeSerialization();
            }

            // Set Base Message Data
            var messageBuilder = new StringBuilder();
            message.Id = (MessageId)messageAttribute.Id;
            message.Timestamp = (uint)Environment.TickCount;

            var validator = _validators[message.GetType()];

            // Fill all Parameters
            AppendParametersFromValidator(AbstractMessageValidator, message, messageBuilder);
            AppendParametersFromValidator(validator, message, messageBuilder, parametersToTake);

            messageBuilder.Remove(messageBuilder.Length - 1, 1); // remove last whitespace
            messageBuilder.Append('\n');

            return messageBuilder.ToString();
        }

        private void AppendParametersFromValidator(MessageValidator validator, IMessage message, StringBuilder messageBuilder, int parametersToTake = -1)
        {
            var limit = parametersToTake >= 0
                ? parametersToTake
                : validator.Parameters.Length;

            for (var i = 0; i < limit; i++)
            {
                try
                {
                    if (!AppendParametersFromValue(validator, validator.Parameters[i], message, messageBuilder))
                        break;
                }
                catch (MessageProcessorException exception)
                {
                    throw new MessageProcessorException(string.Format("Could not append parameter from validator (index = {0})", i), exception);
                }
            }
        }

        private bool AppendParametersFromValue(MessageValidator validator, object value, IMessage message, StringBuilder messageBuilder)
        {
            var parameter = value as Parameter;

            if (parameter != null)
            {
                var param = parameter;
                if (param.HasAttribute<ParameterFromAttribute>())
                {
                    var sourceIndex = param.GetAttribute<ParameterFromAttribute>().Index;
                    return AppendParametersFromValue(validator, validator.Parameters[sourceIndex].GetValueFromTarget(message), message, messageBuilder);
                }

                var paramValue = param.GetValueFromTarget(message);
                if (paramValue != null)
                {
                    if (paramValue.GetType().IsArray && !param.HasAttribute<NoArrayCountAttribute>())
                    {
                        AppendParametersFromValue(validator, ((Array)paramValue).Length, message, messageBuilder);
                    }

                    return AppendParametersFromValue(validator, paramValue, message, messageBuilder);
                }

                if (param.GetAttribute<ParameterAttribute>().Required)
                {
                    throw new MessageProcessorException("Required field is null");
                }

                return false;
            }

            if (value.GetType().IsArray)
            {
                foreach (var valueElement in (object[])value)
                {
                    AppendParametersFromValue(validator, valueElement, message, messageBuilder);
                }
            }
            else if (value is IMessage)
            {
                AppendParametersFromValidator(_nestedValidators[value.GetType()], (IMessage)value, messageBuilder);
            }
            else if (value is bool)
            {
                messageBuilder.AppendFormat("{0} ", (bool)value ? 1 : 0);
            }
            else
            {
                messageBuilder.AppendFormat("{0} ", value.ToString().Replace('\x20', '\x1D'));
            }

            return true;
        }
    }
}