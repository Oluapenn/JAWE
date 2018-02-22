using System;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messaging.Resolving
{
    public class ResolvedMessagesSet
    {
        public Type Type { get; }
        public MessageAttribute MessageAttribute { get; }

        public ResolvedMessagesSet(Type type, MessageAttribute messageAttribute)
        {
            Type = type;
            MessageAttribute = messageAttribute;
        }
    }
}
