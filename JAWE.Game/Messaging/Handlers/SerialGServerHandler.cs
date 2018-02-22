using JAWE.Game.Messaging.Builders;
using JAWE.Network.Messages;
using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Game.Messaging.Handlers
{
    [MessageHandler(MessageId.SerialGServ)]
    internal class SerialGServerHandler : GameMessageHandler<SerialGServClient>
    {
        private static readonly ushort ClientVersionNumber = 3;

        public SerialGServerHandler()
        {
            Reject(GameSessionFlags.Handshake);
        }

        protected override bool Process(GameClient sender, SerialGServClient message)
        {
            if (message.Version != ClientVersionNumber)
            {
                sender.Send(SerialGServerResponseBuilder.Build(SerialGameStatusCode.ClientVersionMissmatch));
                sender.Disconnect("Client Version Missmatch.");
                return false;
            }

            // TODO Log MAC Address.
            sender.AddFlag(GameSessionFlags.Handshake);
            sender.Send(SerialGServerResponseBuilder.Build());

            return true;
        }
    }
}
