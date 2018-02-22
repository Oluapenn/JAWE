using System;

namespace JAWE.Network.Messaging.Resolving
{
    public class MessageHandlerResolveException : Exception
    {
        public MessageHandlerResolveException(string message)
            : base(message)
        {

        }
    }
}
