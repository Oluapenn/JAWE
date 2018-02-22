using JAWE.Game.Lobby;
using JAWE.Network.Messages;

namespace JAWE.Game.Messaging.Builders
{
    internal static class KeepaliveBuilder
    {
        public static KeepaliveMessageServer Build(GameClient client)
        {
            return new KeepaliveMessageServer
            {
                Frequency = HearthBeat.Internal,
                RemainingPremiumTime = 2678400, // 31 Days
            };
        }
    }
}

