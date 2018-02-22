using JAWE.Network.Messages;
using JAWE.Network.Messages.States;

namespace JAWE.Game.Messaging.Builders
{
    internal static class SerialGServerResponseBuilder
    {
        public static SerialGServServer Build(SerialGameStatusCode serialGameStatusCode)
        {
            return new SerialGServServer(serialGameStatusCode);
        }

        public static SerialGServServer Build()
        {
            return new SerialGServServer();
        }
    }
}
