using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages
{
    [Message(MessageId.Keepalive, Server = true)]
    public class KeepaliveMessageServer : BaseMessage
    {

        #region Parameters

        [Parameter(0)]
        public int Frequency { get; set; } = 5000;

        [Parameter(1)]
        public int Ping { get; set; } = 0;

        [Parameter(2)]
        public int HackCount { get; set; } = 0;

        [Parameter(3)]
        public int RemainingEventTime { get; set; } = -1;

        [Parameter(4)]
        public int EventType { get; set; } = 0; // 3 = Weekend XP, 4 = XP Event.

        [Parameter(5)]
        public float ExperienceRatio { get; set; } = 1.0f;

        [Parameter(6)]
        public float MoneyRatio { get; set; } = 1.0f;

        [Parameter(7)]
        public int RemainingPremiumTime { get; set; } = -1;

        #endregion

    }
}
