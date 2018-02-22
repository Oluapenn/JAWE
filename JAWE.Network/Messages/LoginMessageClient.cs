using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Resolving;

namespace JAWE.Network.Messages
{
    [Message(MessageId.Login, ParseServer.Login)]
    public class LoginMessageClient : BaseMessage
    {

        #region Parameters
        
        [Parameter(0)]
        public uint Crc { get; set; }

        [Parameter(1)]
        public byte ExecutionType { get; set; }

        [Parameter(2), MaxLength(16)]
        public string Username { get; set; }

        [Parameter(3), MaxLength(16)]
        public string Password { get; set; }

        [Parameter(4), MaxLength(20)]
        public string Displayname;

        [Parameter(5)]
        public byte Gender; // TODO Add Between Attribute

        [Parameter(6, false)]
        public byte? Age; // TODO Add Between Attribute

        #endregion

    }
}
