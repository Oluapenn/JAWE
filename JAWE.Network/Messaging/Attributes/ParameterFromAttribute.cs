using System;

namespace JAWE.Network.Messaging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ParameterFromAttribute : AbstractMessageAttribute
    {
        public ParameterFromAttribute(int index)
        {
            Index = index;
        }

        public int Index { get; private set; }
    }
}