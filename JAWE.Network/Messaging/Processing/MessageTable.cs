using System;
using System.Collections.Generic;
using JAWE.Network.Messaging.Resolving;

namespace JAWE.Network.Messaging.Processing
{
    public class MessageTable
    {
        private readonly Dictionary<ushort, IMessageHandler> _messageHandlers;

        public MessageTable()
        {
            _messageHandlers = new Dictionary<ushort, IMessageHandler>();
            Initialize();
        }

        public void Initialize()
        {
            _messageHandlers.Clear();

            var resolvedHandlers = MessageHandlerResolver.Resolve();
            AddHandlers(resolvedHandlers);
        }

        private void AddHandlers(IEnumerable<MessageHandlerResolverResultSet> messageHandlers)
        {
            foreach (var resultSet in messageHandlers)
            {
                var handlerId = (ushort)resultSet.MessageId;

                if (_messageHandlers.ContainsKey(handlerId))
                    continue;

                var handlerInstance = Activator.CreateInstance(resultSet.HandlerType) as IMessageHandler;
                if (handlerInstance != null)
                {
                    _messageHandlers.Add(handlerId, handlerInstance);
                }
            }
        }

        public IMessageHandler Find(MessageId messageId)
        {
            var messageIdValue = (ushort) messageId;

            return _messageHandlers.ContainsKey(messageIdValue)
                ? _messageHandlers[messageIdValue]
                : null;
        }

        public int HandlerCount => _messageHandlers.Count;
    }
}
