using JAWE.Network.Messages.Parts;
using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages
{
    [Message(MessageId.SetChannel, Server = true)]
    public class SetChannelMessageServer : BaseMessage
    {

        #region Parameters

        [Parameter(0)]
        public SetChannelStatusCode SetChannelStatus { get; set; }

        [Parameter(1)]
        public ChannelType TargetChannel { get; set; }

        #endregion

        #region Constructor

        public SetChannelMessageServer()
        {
            SetChannelStatus = SetChannelStatusCode.Success;
        }

        #endregion

    }
}
