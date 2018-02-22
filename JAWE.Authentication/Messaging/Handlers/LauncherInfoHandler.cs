using JAWE.Network.Messages;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Authentication.Messaging.Handlers
{
    [MessageHandler(MessageId.LauncherInformation)]
    internal class LauncherInfoHandler : LoginMessageHandler<LaunchInfoMessageClient>
    {
        public LauncherInfoHandler()
        {
            Reject(LoginSessionFlags.Authenticated);
        }

        protected override bool Process(LoginClient sender, LaunchInfoMessageClient message)
        {
            var launcherMessage = new LaunchInfoMessageServer
            {
                Format = 0,
                LauncherVersion = 0,
                UpdaterVersion = 0,
                ClientVersion = 0,
                SubVersion = 0,
                Option = 0,
                PatchUrl = "http://127.0.0.1/"
            };

            sender.Send(launcherMessage);

            return false;
        }
    }
}
