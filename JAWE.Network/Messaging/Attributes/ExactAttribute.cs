using System;

namespace JAWE.Network.Messaging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ExactAttribute : AbstractMessageAttribute
    {
        public ExactAttribute(object exactMatch)
        {
            ExactMatch = exactMatch;
        }

        public object ExactMatch { get; private set; }
    }
}