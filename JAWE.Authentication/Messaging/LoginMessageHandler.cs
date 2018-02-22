using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Authentication.Messaging
{
    internal abstract class LoginMessageHandler<TMessageType> : MessageHandler<LoginClient, LoginSessionFlags, TMessageType>
        where TMessageType : IMessage
    {

    }
}
