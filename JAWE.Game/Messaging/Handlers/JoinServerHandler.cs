using System;
using JAWE.Game.Messaging.Builders;
using JAWE.Network.Messages;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Game.Messaging.Handlers
{
    [MessageHandler(MessageId.JoinServer)]
    internal class JoinServerHandler : GameMessageHandler<JoinServerMessageClient>
    {
        public JoinServerHandler()
        {
            Allow(GameSessionFlags.Handshake);
            Reject(GameSessionFlags.Authenticated);
        }

        protected override bool Process(GameClient sender, JoinServerMessageClient message)
        {
            // NOTE We should not use the user id in any 'public' packets so we'll just send 0 instead and use the username & token to authenticate.
            Console.WriteLine("Join Server for Username: {0}, Token: {1}", message.Username, message.Token);

            sender.AddFlag(GameSessionFlags.Authenticated);

            sender.Send(JoinServerResponseBuilder.Build(sender));
            sender.Send(KeepaliveBuilder.Build(sender));

            return true;
        }
    }
}
