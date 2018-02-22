using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages
{
    [Message(MessageId.SerialGServ)]
    public class SerialGServClient : BaseMessage
    {

        #region Parameters

        [Parameter(0), Exact("dla#qud$wlr%aks^tp&")]
        public string Constant { get; set; }

        [Parameter(1)]
        public ushort Version { get; set; }

        [Parameter(2), Match("^[0-9a-fA-F]{12}$")]
        public string MacAddress { get; set; }

        #endregion

    }
}
