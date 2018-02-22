using JAWE.Network.Messages;
using JAWE.Network.Messages.Parts;
using JAWE.Network.Messages.States;

namespace JAWE.Game.Messaging.Builders
{
    internal static class SetChannelResponseBuilder
    {
        public static SetChannelMessageServer Build(GameClient client)
        {
            return new SetChannelMessageServer
            {
                TargetChannel = client.Channel?.Id ?? ChannelType.None
            };
        }
    }
}
