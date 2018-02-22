using JAWE.Test.Messaging;
using JAWE.Test.Messaging.Attributes;

namespace JAWE.Test
{
    class TestMessage : IMessage
    {
        [Parameter(0)]
        public int Value { get; set; }

        [Parameter(1)]
        public bool Magic { get; set; }
    }
}
