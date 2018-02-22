using System;
using System.Collections.Generic;
using System.Text;
using JAWE.Game.Messaging;
using JAWE.Network.Messages.States;

namespace JAWE.Game.Lobby
{
    internal class Room
    {
        public ushort Id { get; private set; }
        public RoomState State { get; private set; }

        public void Remove(GameClient client)
        {
            // TODO Implement
        }
    }
}
