using JAWE.Network.Messages.Parts;
using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Resolving;

namespace JAWE.Network.Messages
{
    [Message(MessageId.SetChannel, ParseServer.Game)]
    public class SetChannelMessageClient : BaseMessage
    {

        #region Parameters

        [Parameter(0)]
        public ChannelType Channel { get; set; }

        #endregion

    }
}
