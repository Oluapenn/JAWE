using System;
using JAWE.Network;
using JAWE.Network.Codec;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Authentication.Messaging
{
    internal class LoginClient : WrAbstractClient<LoginSessionFlags>
    {
        public static readonly MessageTable MessageTable = new MessageTable();
        private static readonly WrMessageCodec WrEncoder = new WrMessageCodec(0xC3, 0x96);
       
        public LoginClient()
            : base(WrEncoder)
        {
            MessageReceived +=  OnMessageReceived;
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
