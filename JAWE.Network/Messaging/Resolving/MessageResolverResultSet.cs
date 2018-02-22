using System;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messaging.Resolving
{
    internal class MessageResolverResultSet
    {
        public MessageId Id { get; }
        public MessageAttribute MessageAttribute { get; }
        public Type Type { get; }

        public MessageResolverResultSet(MessageId id, Type type, MessageAttribute messageAttribute)
        {
            Id = id;
            Type = type;
            MessageAttribute = messageAttribute;
        }
    }
}
