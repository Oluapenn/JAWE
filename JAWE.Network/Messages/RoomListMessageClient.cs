using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Resolving;

namespace JAWE.Network.Messages
{
    [Message(MessageId.RoomList, ParseServer.Game)]
    public class RoomListMessageClient : BaseMessage
    {

        #region Parameters

        [Parameter(0)]
        public byte Page { get; set; }

        [Parameter(1)]
        public bool WaitingOnly { get; set; }

        [Parameter(2)]
        public byte PreviousPage { get; set; }

        #endregion

    }
}
