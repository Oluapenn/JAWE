using System;
using System.Collections.Generic;
using JAWE.Game.Lobby;
using JAWE.Network.Messages;
using JAWE.Network.Messages.Parts;
using JAWE.Network.Messages.States;

namespace JAWE.Game.Messaging.Builders
{
    internal static class RoomListBuilder
    {
        public static RoomListMessageServer Build(GameClient sender, byte page, bool waitingOnly, IEnumerable<Room> roomList)
        {
            return new RoomListMessageServer
            {
                Page = page,
                WaitingOnly = waitingOnly,
                RoomsList = new[]
                {
                    new RoomInfo
                    {
                        Id = 0,
                        Name = "Hello JAWE #0",
                        State = RoomState.Waiting,
                        BattleMode = BattleMode.Deathmatch,
                        Map = 12,
                        MaxiumPlayers = 8,
                        SuperHostType = SuperHostType.Type2,
                    },
                    new RoomInfo
                    {
                        Id = 3,
                        Name = "Hello JAWE #3",
                        State = RoomState.Waiting,
                        BattleMode = BattleMode.FreeForAll,
                        Level = 2,
                        Map = 0,
                        MaxiumPlayers = 8,
                        SuperHostType = SuperHostType.Type1,
                    },
                    new RoomInfo
                    {
                        Id = 5,
                        Name = "Hello JAWE",
                        State = RoomState.Waiting,
                        BattleMode = BattleMode.FreeForAll,
                        Level = 2,
                        Map = 0,
                        MaxiumPlayers = 8,
                        SuperHostType = SuperHostType.Both,
                    }
                }
            };
        }
    }
}
