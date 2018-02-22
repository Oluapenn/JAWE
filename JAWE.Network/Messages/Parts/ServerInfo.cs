using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages.Parts
{
    /// <summary>
    /// Structure that contains information of a gameserver
    /// </summary>
    public class ServerInfo : IMessage
    {
        /// <summary>
        /// The id of the game server.
        /// </summary>
        [Parameter(0)]
        public int Id { get; set; }

        /// <summary>
        /// The Displayname of the GameServer.
        /// </summary>
        [Parameter(1)]
        public string Name { get; set; }

        /// <summary>
        /// The IP-Address of the game server.
        /// </summary>
        [Parameter(2)]
        public string Address { get; set; }

        /// <summary>
        /// This parameter is ignored by the client.
        /// </summary>
        [Parameter(3)]
        public ushort Port { get; set; }

        /// <summary>
        /// The amount of players currently active on the game server.
        /// </summary>
        [Parameter(4)]
        public ushort Players { get; set; }

        /// <summary>
        /// The type of game server.
        /// </summary>
        [Parameter(5)]
        public ServerType Type { get; set; }
    }
}
