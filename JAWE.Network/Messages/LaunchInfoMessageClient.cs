using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Resolving;

namespace JAWE.Network.Messages
{
    [Message(MessageId.LauncherInformation, ParseServer.Login)]
    public class LaunchInfoMessageClient : BaseMessage
    {

        // This message has no parameters.

    }
}
