using System;

namespace JAWE.Network.Messaging.Resolving
{
    internal class MessageHandlerResolverResultSet
    {
        public MessageId MessageId { get; }
        public Type HandlerType { get; }

        public MessageHandlerResolverResultSet(MessageId messageId, Type handlerType)
        {
            MessageId = messageId;
            HandlerType = handlerType;
        }
    }
}