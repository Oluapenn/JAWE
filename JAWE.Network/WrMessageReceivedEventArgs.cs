using System;
using JAWE.Network.Messages;

namespace JAWE.Network
{
    public class WrMessageReceivedEventArgs : EventArgs
    {
        public BaseMessage Message { get; }

        public WrMessageReceivedEventArgs(BaseMessage message)
        {
            Message = message;
        }
    }
}