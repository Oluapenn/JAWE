using System;
using JAWE.Network.Messaging.Resolving;

namespace JAWE.Network.Messaging.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageAttribute : AbstractMessageAttribute
    {
        public ushort Id { get; }
        public ParseServer ParseServer { get; }
        public bool Server { get; set; }

        public MessageAttribute(MessageId id)
            : this((ushort)id, ParseServer.None)
        {

        }

        public MessageAttribute(MessageId id, ParseServer parseServer)
            : this((ushort)id, parseServer)
        {

        }

        public MessageAttribute(ushort id, ParseServer parseServer)
        {
            Id = id;
            ParseServer = parseServer;
            Server = false;
        }
    }
}