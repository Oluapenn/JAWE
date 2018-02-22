using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Resolving;

namespace JAWE.Network.Messages
{
    [Message(MessageId.Keepalive, ParseServer.Game)]
    public class KeepaliveMessageClient : BaseMessage
    {

        #region Parameters
        
        [Parameter(0)]
        public ushort SessionId { get; set; }

        [Parameter(1)]
        public long TimeStamp { get; set; }

        [Parameter(2)]
        public int GrenadeCount { get; set; }
        
        #endregion

    }
}
