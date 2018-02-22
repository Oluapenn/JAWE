using JAWE.Network.Messages;
using JAWE.Network.Messages.Parts;
using JAWE.Network.Messages.States;

namespace JAWE.Authentication.Messaging.Builders
{
    public static class LoginResponseBuilder
    {
        public static LoginMessageServer CreateLoginResponse(LoginStatusCode statusCode)
        {
            return new LoginMessageServer(statusCode);
        }

        public static LoginMessageServer CreateLoginResponse()
        {
            return new LoginMessageServer
            {
                UserId = 20,
                Username = "Dev",
                Displayname = "Developer",
                Token = "Magic",
                Servers = new[]
                {
                    new ServerInfo
                    {
                        Id = 1,
                        Name = "JAWE",
                        Address = "127.0.0.1",
                        Port = 5340, // Unused by the client.
                        Players = 0,
                        Type = ServerType.Normal,
                    }
                }
            };
        }
    }
}
