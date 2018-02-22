using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages.Parts
{
    public class ClanBattleInfo : IMessage
    {
        /// <summary>
        /// Clan Battle Key -> Next block is ignored if this is 0 (but needs to be send) .
        /// </summary>
        [Parameter(0)]
        public string Key { get; set; } = "0";

        /// <summary>
        /// Target server id for clan battle - Note: Server Type needs to be 2!
        /// </summary>
        [Parameter(1)]
        public byte ServerId { get; set; } = 0;
    }
}
