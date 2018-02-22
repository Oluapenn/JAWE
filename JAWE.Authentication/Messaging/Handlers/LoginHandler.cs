using System;
using JAWE.Network.Messages;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Authentication.Messaging.Builders;

namespace JAWE.Authentication.Messaging.Handlers
{
    [MessageHandler(MessageId.Login)]
    internal class LoginHandler : LoginMessageHandler<LoginMessageClient>
    {
        public LoginHandler()
        {
            Reject(LoginSessionFlags.Authenticated);
        }

        protected override bool Process(LoginClient sender, LoginMessageClient message)
        {
            Console.WriteLine("Received login request for Username: {0}, Password: {1}", message.Username, message.Password);
            
            //var response = LoginResponseBuilder.CreateLoginResponse(LoginStatusCode.Blocked);
            var response = LoginResponseBuilder.CreateLoginResponse();
            sender.Send(response);

            return true;
        }
    }
}
