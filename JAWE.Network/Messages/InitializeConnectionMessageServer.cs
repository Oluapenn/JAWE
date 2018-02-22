using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages
{
    [Message(MessageId.InitializeConnection, Server = true)]
    public class InitializeConnectionMessageServer : BaseMessage
    {

        #region Parameters
        
        [Parameter(0)]
        public int CrcSeed { get; set; } = 0;

        [Parameter(1)]
        public int Version { get; set; } = 77;

        #endregion

    }
}
