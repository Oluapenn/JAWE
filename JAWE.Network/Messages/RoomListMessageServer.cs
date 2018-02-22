using JAWE.Network.Messages.Parts;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Network.Messages
{
    [Message(MessageId.RoomList, Server = true)]
    public class RoomListMessageServer : BaseMessage, IMessageSerialization
    {

        #region Parameters

        [Parameter(0)]
        public int Count { get; set; }

        [Parameter(1)]
        public byte Page { get; set; }

        [Parameter(2)]
        public bool WaitingOnly { get; set; }

        [Parameter(3), NoArrayCount]
        public RoomInfo[] RoomsList { get; set; }

        #endregion

        #region Serialization

        public void Deserialized()
        {

        }

        public int BeforeSerialization()
        {
            Count = RoomsList.Length;
            return -1;
        }

        #endregion

    }
}
