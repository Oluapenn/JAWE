using JAWE.Game.Lobby;
using JAWE.Game.Messaging.Builders;
using JAWE.Network.Messages;
using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Game.Messaging.Handlers
{
    [MessageHandler(MessageId.SetChannel)]
    internal class SetChannelHandler : GameMessageHandler<SetChannelMessageClient>
    {
        public SetChannelHandler()
        {
            Allow(GameSessionFlags.Authenticated);
            Reject(GameSessionFlags.Room);
        }

        protected override bool Process(GameClient sender, SetChannelMessageClient message)
        {
            // Figure out if the client does not know which channel to select.
            if (message.Channel == ChannelType.None || message.Channel == ChannelType.None2)
            {
                message.Channel = Channels.AiConfig != AiServerType.AiOnly
                    ? ChannelType.CQC
                    : ChannelType.Ai;
            }

            // Find target channel.
            var targetChannel = Channels.Get(message.Channel);

            if (targetChannel == null)
                return false;

            // Remove from current channel if set.
            sender.Channel?.Remove(sender);

            if (!targetChannel.Add(sender))
                return false;

            sender.Send(SetChannelResponseBuilder.Build(sender));

            var roomList = sender.Channel.RoomsOnPage(0, false);
            sender.Send(RoomListBuilder.Build(sender, 0, false, roomList));

            return true;
        }
    }
}
