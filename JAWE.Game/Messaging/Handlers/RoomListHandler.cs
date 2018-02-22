using JAWE.Game.Messaging.Builders;
using JAWE.Network.Messages;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Game.Messaging.Handlers
{
    [MessageHandler(MessageId.RoomList)]
    internal class RoomListHandler : GameMessageHandler<RoomListMessageClient>
    {
        public RoomListHandler()
        {
            Allow(GameSessionFlags.Authenticated);
            Reject(GameSessionFlags.Room);
        }

        protected override bool Process(GameClient sender, RoomListMessageClient message)
        {
            if (sender.Channel == null)
                return true; // Ignore

            var roomList = sender.Channel.RoomsOnPage(message.Page, message.WaitingOnly);
            
            sender.Send(RoomListBuilder.Build(sender, message.Page, message.WaitingOnly, roomList));

            return true;
        }
    }
}
