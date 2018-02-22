using System;
using System.Linq;
using System.Reflection;
using System.Text;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages
{
    public class BaseMessage : IMessage
    {

        #region Parameters

        [Parameter(0)]
        public uint Timestamp { get; set; }

        [Parameter(1)]
        public MessageId Id { get; set; }

        #endregion

        #region ToString

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(GetType().Name);
            stringBuilder.Append(" (");

            var propertyInfo =
                from PropertyInfo p in
                GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                let attrib = (ParameterAttribute)p.GetCustomAttributes(typeof(ParameterAttribute), false).First()
                orderby attrib.Index
                select new Tuple<string, string>(p.Name, (p.GetValue(this, null) ?? "(null)").ToString());

            var enumerable = propertyInfo.ToList();
            if (enumerable.Count > 0)
            {
                foreach (var tuple in enumerable)
                {
                    stringBuilder.AppendFormat("{0} = {1}, ", tuple.Item1, tuple.Item2);
                }

                stringBuilder.Length -= 2;
            }

            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        #endregion
        
    }
}