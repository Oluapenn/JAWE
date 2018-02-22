using System;

namespace JAWE.Test.Messaging.Parsing
{
    public class MessageParseException : Exception
    {
        public MessageParseException(string message) : base(message)
        {
        }
    }
}
