using System;
using System.Collections.Generic;
using System.Text;
using JAWE.Test.Messaging;

namespace JAWE.Test
{
    class BaseMessage : IMessage
    {
        public int Value { get; set; }
        public bool Magic { get; set; }
    }
}
