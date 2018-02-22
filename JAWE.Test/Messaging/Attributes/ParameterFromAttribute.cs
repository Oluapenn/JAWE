using System;

namespace JAWE.Test.Messaging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ParameterFromAttribute : AbstractMessageAttribute
    {

        public int Index { get; }

        public ParameterFromAttribute(int index)
        {
            Index = index;
        }

    }
}