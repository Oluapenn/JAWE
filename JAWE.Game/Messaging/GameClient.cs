using System;
using JAWE.Game.Lobby;
using JAWE.Network;
using JAWE.Network.Codec;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Game.Messaging
{
    internal class GameClient : WrAbstractClient<GameSessionFlags>
    {
        public static readonly MessageTable MessageTable = new MessageTable();
        public static readonly WrMessageCodec WrEncoder = new WrMessageCodec(0xC3, 0x96);
        
        public Channel Channel { get; set; }
        public Room Room { get; private set; }

        public GameClient()
            : base(WrEncoder)
        {
            Disconnected += OnDisconnected;
            MessageReceived += OnMessageReceived;
        }

        public void SetRoom(Room room)
        {
            Room = room;

            if (Room != null)
            {
                AddFlag(GameSessionFlags.Room);
            }
            else
            {
                RemoveFlag(GameSessionFlags.Room);
            }
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs disconnectedEventArgs)
        {
            // Remove current client from room if it's set.
            Room?.Remove(this);

            // Remove current client from channel if it's set.
            Channel?.Remove(this);
        }

        private void OnMessageReceived(object sender, WrMessageReceivedEventArgs args)
        {
            var message = args.Message;

            Console.WriteLine("Received Message >> {0}", message);

            var handler = MessageTable.Find(message.Id);
            if (handler == null)
                return;

            if (!handler.Handle(this, message))
            {
                Disconnect("Message handler returned false.");
            }
        }
    }
}
