using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using JAWE.Game.Messaging;
using JAWE.Network.Messages.States;

namespace JAWE.Game.Lobby
{
    internal class Channel
    {

        #region Variables

        public ChannelType Id { get;}

        private readonly ConcurrentDictionary<ushort, GameClient> _clients;
        private readonly ConcurrentDictionary<ushort, Room> _rooms;

        #endregion

        #region Constructor

        public Channel(ChannelType id)
        {
            Id = id;
            _clients = new ConcurrentDictionary<ushort, GameClient>();
            _rooms = new ConcurrentDictionary<ushort, Room>();
        }

        #endregion

        #region Clients

        public bool Add(GameClient client)
        {
            Remove(client);

            if (!_clients.TryAdd(client.ConnectionSlot, client))
                return false;

            client.Channel = this;

            Console.WriteLine("{0} joined channel: {1}", "User", Id, this);

            return true;
        }

        public void Remove(GameClient client)
        {
            if (!_clients.TryRemove(client.ConnectionSlot, out var oldClient))
                return;

            Console.WriteLine("{0} left channel: {1}", "User", Id, oldClient.Channel);
            oldClient.Channel = null;
        }

        public IEnumerable<GameClient> Clients()
        {
            return _clients.Values;
        }

        #endregion

        #region Rooms

        public bool Add(Room room)
        {
            return _rooms.TryAdd(room.Id, room);
        }

        public void Remove(Room room)
        {
            _rooms.TryRemove(room.Id, out var _);
        }
        
        public IEnumerable<Room> RoomsOnPage(byte page, bool waitingOnly)
        {
            if (waitingOnly)
            {
                return _rooms.Values
                    .Where(room => room.State == RoomState.Waiting)
                    .Skip(16 * page)
                    .Take(16);
            }
            
            var beginRange = page * 16;
            var endRange = beginRange + 16;
            
            return _rooms.Values.Where(room => room.Id >= beginRange && room.Id <= endRange);
        }

        #endregion

        public override string ToString()
        {
            return $"Channel(Id: {Id}, Clients: {_clients.Count}, Rooms: {_rooms.Count})";
        }
    }
}
