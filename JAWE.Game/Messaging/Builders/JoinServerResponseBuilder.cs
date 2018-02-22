using JAWE.Game.Lobby;
using JAWE.Network.Messages;
using JAWE.Network.Messages.States;

namespace JAWE.Game.Messaging.Builders
{
    internal static class JoinServerResponseBuilder
    {
        public static JoinServerMessageServer Build(GameClient client)
        {
            return new JoinServerMessageServer
            {
                UserId = 1,
                SessionId = client.ConnectionSlot,
                SerialNumber = client.ConnectionSlot,
                Displayname = "Developer",
                Premium = PremiumType.Diamond,
                Level = 1,
                Experience = 0,
                Money = 50000,
                Kills = 10,
                Deaths = 5,

                // Ai Config
                AiState = Channels.AiConfig,
            };
        }
    }
}
