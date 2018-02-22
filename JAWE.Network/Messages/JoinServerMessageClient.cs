using JAWE.Network.Messages.Parts;
using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages
{
    [Message(MessageId.JoinServer)]
    public class JoinServerMessageClient : BaseMessage
    {

        #region Parameters

        [Parameter(0)]
        public int UserId { get; set; }

        [Parameter(1)]
        public int NxCode;

        [Parameter(2), MinLength(3), MaxLength(16)]
        public string Username { get; set; }

        [Parameter(3), MinLength(3), MaxLength(16)]
        public string Displayname { get; set; }

        [Parameter(4)]
        public ushort Gender;

        [Parameter(5)]
        public ushort Age;

        /// <summary>
        /// WR Point System used to measure the 'performance' of your computer. It is inaccurate.
        /// </summary>
        [Parameter(6)]
        public int SystemScore;

        [Parameter(7)]
        public PermissionStatus Permission { get; set; }

        [Parameter(8)]
        public int ClanId { get; set; }

        [Parameter(9)]
        public int ClanUserLevel { get; set; }

        [Parameter(10)]
        public int ClanIcon { get; set; }

        [Parameter(11), MaxLength(24)]
        public string ClanName { get; set; }

        [Parameter(12)]
        public int BattleSerial;

        /// <summary>
        /// Seems to be hardcoded to 0 inside the client
        /// </summary>
        [Parameter(13), Exact(0)]
        public int _13;

        [Parameter(14)]
        public string Token { get; set; }

        [Parameter(15), Exact("dnjfhr^")]
        public string _Constant { get; set; }

        #endregion

    }
}
