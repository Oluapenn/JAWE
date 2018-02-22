using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages.Parts
{
    public class RoomInfo : IMessage
    {

        #region Parameters

        [Parameter(0)]
        public int Id { get; set; }

        [Parameter(1)]
        public RoomState State { get; set; }

        [Parameter(2), ParameterFrom(1)]
        public RoomState _2;

        [Parameter(3)]
        public byte MasterIndex { get; set; } = 0;

        [Parameter(4), MaxLength(64)]
        public string Name { get; set; } = "JAWE";

        [Parameter(5)]
        public bool HasPassword { get; set; } = false;

        [Parameter(6), MinLength(4), MaxLength(32)]
        public byte MaxiumPlayers { get; set; } = 0;

        [Parameter(7), MinLength(0), MaxLength(32)]
        public byte PlayerCount { get; set; } = 0;

        [Parameter(8)]
        public int Map { get; set; } = 0;

        [Parameter(9)]
        public byte RoundLimit { get; set; } = 0;

        [Parameter(10)]
        public byte KillLimit { get; set; } = 0;

        [Parameter(11)]
        public byte TimeLimit { get; set; } = 0;

        [Parameter(12)]
        public BattleMode BattleMode { get; set; } = BattleMode.Explosive;

        [Parameter(13)]
        public byte MujukTime { get; set; } = 0; // mujuk time; doesn't affect game

        [Parameter(14)]
        public JoinState JoinState { get; set; } = JoinState.Joinable; // 2 = Joinable

        [Parameter(15)]
        public int Level { get; set; }

        [Parameter(16)]
        public SuperHostType SuperHostType { get; set; } = SuperHostType.None;

        [Parameter(17)]
        public int FlowMode { get; set; } = 4; // flow mode; doesn't affect game

        [Parameter(18)]
        public LevelLimit LevelLimit { get; set; } = LevelLimit.None;

        [Parameter(19)]
        public bool IsPremiumOnly { get; set; } = false;

        [Parameter(20)]
        public bool IsVotekickEnabled { get; set; } = true;

        [Parameter(21)]
        public bool IsAutoStartEnabled { get; set; } = false;

        [Parameter(22)]
        public int AveragePing { get; set; } = 0;

        [Parameter(23)]
        public byte PingLimit { get; set; } = 0;

        [Parameter(24)]
        public string ClanWarKey = "-1"; // TODO If > -1 Add Clan War Data.

        #endregion

    }
}
