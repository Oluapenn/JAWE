using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages.Parts
{
    public class ClanInfo : IMessage
    {
        [Parameter(0)]
        public int Id { get; set; }

        [Parameter(1)]
        public string Name { get; set; }

        [Parameter(2)]
        public int Rank { get; set; }

        [Parameter(3)]
        public int UserRank { get; set; }
    }
}
