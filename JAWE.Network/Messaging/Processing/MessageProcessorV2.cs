using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Resolving;
using JAWE.Network.Messaging.Validators;

namespace JAWE.Network.Messaging.Processing
{
    public class MessageProcessorV2
    {

        #region Static

        private static readonly Dictionary<Type, IParameter> PrimitiveValidators;

        static MessageProcessorV2()
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
        }

        #endregion

        private readonly Dictionary<ushort, Type> _parsableMessages;
        private readonly Dictionary<Type, MessageAttribute> _messageAttributes;
        private readonly Dictionary<Type, MessageValidator> _nestedValidators;
        private readonly Dictionary<Type, MessageValidator> _validators;

        public MessageProcessorV2(IMessageResolver resolver)
        {
            _parsableMessages = new Dictionary<ushort, Type>();
            _messageAttributes =  new Dictionary<Type, MessageAttribute>();
            _validators = new Dictionary<Type, MessageValidator>();
            _nestedValidators = new Dictionary<Type, MessageValidator>();

            var resolvedMessages = resolver.Resolve();
            AddMessages(resolvedMessages);

            Console.WriteLine("Parsable Messages Cached: {0}", _parsableMessages.Count);
            Console.WriteLine("Attributes Cached: {0}", _messageAttributes.Count);
        }

        private void AddMessages(IEnumerable<ResolvedMessagesSet> messages)
        {
            foreach (var message in messages)
            {
                if (message.MessageAttribute == null)
                    continue;

                var attribute = message.MessageAttribute;
                if (!attribute.Server)
                {
                    // Only cache parsable messages.
                    _parsableMessages.Add(attribute.Id, message.Type);
                }
                
                // Cache attributes
                _messageAttributes.Add(message.Type, attribute);

                var validator = new MessageValidator(message.Type);
                _validators.Add(message.Type, validator);

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

        public MessageValidationResult MatchNestedType(Type nestedType, string[] parameters, int offset)
        {
            throw new NotImplementedException();
            /*if (PrimitiveValidators.ContainsKey(nestedType))
            {
                var result = PrimitiveValidators[nestedType].Validate(null, parameters[offset]);

                return new MessageValidationResult(result.State, 1);
            }

            var validator = _nestedValidators[nestedType];

            return validator.Validate(this, parameters, offset);*/
        }
    }
}
