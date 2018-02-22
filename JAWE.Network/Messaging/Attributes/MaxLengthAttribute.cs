using System;

namespace JAWE.Network.Messaging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class MaxLengthAttribute : AbstractMessageAttribute
    {
        public MaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }

        public int MaxLength { get; private set; }
    }
}