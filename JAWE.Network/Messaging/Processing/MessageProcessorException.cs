using System;

namespace JAWE.Network.Messaging.Processing
{
    public class MessageProcessorException : Exception
    {
        public MessageProcessorException(string reason)
            : base(reason)
        {
        }

        public MessageProcessorException(string reason, Exception innerException)
            : base(reason, innerException)
        {
        }
    }
}