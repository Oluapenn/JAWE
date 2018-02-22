using System;

namespace JAWE.Network.Messaging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class MatchAttribute : AbstractMessageAttribute
    {
        public MatchAttribute(string expression)
        {
            Expression = expression;
        }

        public string Expression { get; private set; }
        public string ExtractGroup { get; set; }
    }
}