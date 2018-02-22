using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages
{
    [Message(MessageId.LauncherInformation, Server = true)]
    public class LaunchInfoMessageServer : BaseMessage
    {

        #region Parameters

        [Parameter]
        public int Format { get; set; } = 0;

        [Parameter]
        public int LauncherVersion { get; set; } = 0;

        [Parameter]
        public int UpdaterVersion { get; set; } = 0;

        [Parameter]
        public int ClientVersion { get; set; } = 0;

        [Parameter]
        public int SubVersion { get; set; } = 0;

        [Parameter]
        public int Option { get; set; } = 0;

        [Parameter]
        public string PatchUrl { get; set; } = "http://";

        #endregion

    }
}
