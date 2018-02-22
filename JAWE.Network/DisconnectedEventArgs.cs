using System;

namespace JAWE.Network
{
    public class DisconnectedEventArgs : EventArgs
    {
        public string Reason { get; }

        public DisconnectedEventArgs(string reason)
        {
            Reason = reason;
        }
    }
}