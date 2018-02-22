using JAWE.Network.Messages.Parts;
using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Network.Messages
{
    [Message(MessageId.JoinServer, Server = true)]
    public class JoinServerMessageServer : BaseMessage, IMessageSerialization
    {

        #region Parameters

        [Parameter(0)]
        public JoinServerStatus Status { get; set; }

        /// <summary>
        /// Internal Server name, it's not used inside the client.
        /// Maximum size is 512 bytes Packet Wise but it has only 32 bytes reserved in memory.
        /// Maximum Size is = 32 bytes.
        /// </summary>
        [Parameter(1)]
        public string ServerName { get; set; }

        /// <summary>
        /// LB_Idx
        /// </summary>
        [Parameter(2)]
        public ushort SessionId { get; set; }

        /// <summary>
        /// DB_Idx, user id is never used somewhere so potentially you can send 0.
        /// </summary>
        [Parameter(3)]
        public uint UserId { get; set; }

        /// <summary>
        /// Used in the Peer2Peer system (UDP)
        /// </summary>
        [Parameter(4)]
        public uint SerialNumber { get; set; }

        [Parameter(5)]
        public string Displayname { get; set; }

        [Parameter(6)]
        public ClanInfo Clan { get; set; }

        [Parameter(7)]
        public PremiumType Premium { get; set; }

        [Parameter(8)]
        public int ComputerPoints = 2000;

        [Parameter(9)]
        public int FamePoint = 0;

        [Parameter(10)]
        public byte Level { get; set; }

        [Parameter(11)]
        public uint Experience { get; set; }

        [Parameter(12)]
        public int _bp = -1;

        [Parameter(13)]
        public int _score = 0;

        [Parameter(14)]
        public int Money { get; set; }

        [Parameter(15)]
        public uint Kills { get; set; }

        [Parameter(16)]
        public uint Deaths { get; set; }

        [Parameter(17)]
        public int _str = 0;

        [Parameter(18)]
        public int _con = 0;

        [Parameter(19)]
        public int _dex = 0;

        [Parameter(20)]
        public int _stm = 0;

        [Parameter(21)]
        public int _wiz = 0;

        [Parameter(21)]
        public string SlotStates { get; set; }

        [Parameter(22)]
        public EquipmentList Equipment { get; set; }

        [Parameter(23)]
        public string Inventory { get; set; }

        [Parameter(24)]
        public EquipmentList Characters { get; set; }

        [Parameter(25)]
        public string CharacterInventory { get; set; }

        // IF IsGameBangPlayer
        // append(BillOption);
        // append(BillArgument);

        [Parameter(26)]
        public AiServerType AiState { get; set; }

        [Parameter(27)]
        public int _27 = 0;

        [Parameter(28)]
        public int _28 = 0;

        #endregion

        #region Constructors

        public JoinServerMessageServer(JoinServerStatus statusCode)
        {
            Status = statusCode;
        }

        public JoinServerMessageServer()
        {
            ServerName = "JAWE_PRE_ALPHA-0.0.1";
            Status = JoinServerStatus.Success;

            Clan = new ClanInfo
            {
                Id = -1,
                Name = "NULL",
                Rank = -1,
                UserRank = -1,
            };

            SlotStates = "F,F,F,F";

            Equipment = new EquipmentList(new[]
            {
                "DA02,DB01,^,^,^,^,^,^",
                "DA02,DB01,^,^,^,^,^,^",
                "DA02,DB01,^,^,^,^,^,^",
                "DA02,DB01,^,^,^,^,^,^",
                "DA02,DB01,^,^,^,^,^,^",
            });

            Inventory = "^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^";

            Characters = new EquipmentList(new[]
            {
                "BA01,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^",
                "BA02,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^",
                "BA03,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^",
                "BA04,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^",
                "BA05,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^",
            });

            CharacterInventory = "^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^,^";
            AiState = AiServerType.Disabled;
        }

        #endregion

        #region Serialization

        public void Deserialized()
        {
        }

        public int BeforeSerialization()
        {
            return Status != JoinServerStatus.Success ? 1 : -1;
        }

        #endregion

    }
}
