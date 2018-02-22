using System;

namespace JAWE.Network.Messaging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class MinLengthAttribute : AbstractMessageAttribute
    {
        public MinLengthAttribute(int minLength)
        {
            MinLength = minLength;
        }

        public int MinLength { get; private set; }
    }
}