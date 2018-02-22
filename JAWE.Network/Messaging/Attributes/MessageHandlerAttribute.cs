using System;

namespace JAWE.Network.Messaging.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageHandlerAttribute : Attribute
    {
        public MessageId MessageId { get; }

        public MessageHandlerAttribute(MessageId messageId)
        {
            MessageId = messageId;
        }
    }
}
