using JAWE.Network.Messages.Parts;
using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Network.Messages
{
    [Message(MessageId.Login, Server = true)]
    public class LoginMessageServer : BaseMessage, IMessageSerialization
    {

        #region Parameters

        [Parameter(0)]
        public LoginStatusCode ServerStatusCode { get; set; }

        [Parameter(1)]
        public uint UserId { get; set; }

        [Parameter(2)]
        public int _2 = 0; // Unknown

        [Parameter(3)]
        public string Username { get; set; }

        [Parameter(4)]
        public string Password = "0";

        [Parameter(5)]
        public string Displayname { get; set; }

        [Parameter(6)]
        public byte Gender = 1;

        [Parameter(7)]
        public byte Age = 21;

        [Parameter(8)]
        public int _8 = 0; // Unknown

        [Parameter(9)]
        public PermissionStatus Permissions { get; set; }

        [Parameter(10)]
        public string Token { get; set; }

        // Server List
        [Parameter(10)]
        public ServerInfo[] Servers { get; set; }

        // Clan Information
        [Parameter(12)]
        public ClanInfo Clan { get; set; }

        // Clan Battle Information
        [Parameter(13)]
        public ClanBattleInfo ClanBattle { get; set; }

        #endregion

        #region Constructors

        public LoginMessageServer()
        {
            ServerStatusCode = LoginStatusCode.Success;
            Permissions = PermissionStatus.Normal;

            Clan = new ClanInfo
            {
                Id = -1,
                Name = "NULL",
                Rank = -1,
                UserRank = -1,
            };

            ClanBattle = new ClanBattleInfo
            {
                Key = "0",
                ServerId = 0
            };
        }

        public LoginMessageServer(LoginStatusCode serverStatusCode)
        {
            ServerStatusCode = serverStatusCode;
        }

        #endregion

        #region Serialization

        public void Deserialized()
        {
        }

        public int BeforeSerialization()
        {
            return ServerStatusCode != LoginStatusCode.Success
                ? 1
                : -1;
        }

        #endregion

    }
}
