using JAWE.Network;

namespace JAWE.Authentication.Messaging
{
    internal class LoginServer : TcpServer<LoginClient>
    {
        public LoginServer()
            : base(5330, 100)
        {

        }
    }
}
