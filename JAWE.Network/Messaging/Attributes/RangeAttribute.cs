using System;

namespace JAWE.Network.Messaging.Attributes
{
    /// <summary>
    /// Range from 1-3 allows values: 1, 2, 3
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class RangeAttribute : AbstractMessageAttribute
    {
        public RangeAttribute(double minimum, double maximum)
            : base()
        {
            Minimum = (decimal) minimum;
            Maximum = (decimal) maximum;
        }

        public decimal Minimum { get; private set; }
        public decimal Maximum { get; private set; }
    }
}