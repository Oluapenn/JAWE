using System;
using System.Runtime.CompilerServices;

namespace JAWE.Test.Messaging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class ParameterAttribute : AbstractMessageAttribute
    {
        public int Index { get; }
        public bool Required { get; }

        public ParameterAttribute([CallerLineNumber] int index = 0, bool required = true)
        {
            Index = index;
            Required = required;
        }

    }
}