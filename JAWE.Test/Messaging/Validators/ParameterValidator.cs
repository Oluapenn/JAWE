using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JAWE.Test.Messaging.Attributes;

namespace JAWE.Test.Messaging.Validators
{
    class ParameterValidator
    {
        public Type MessageType { get; }
        public Parameter[] Parameters { get; }

        public ParameterValidator(Type messageType)
        {
            MessageType = messageType;

            var parameters = new List<Parameter>();

            parameters.AddRange(ExtractParameters(messageType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)));
            parameters.AddRange(ExtractParameters(messageType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)));

            Parameters = (from Parameter param in parameters let index = param.GetAttribute<ParameterAttribute>().Index orderby index select param).ToArray();
        }

        private static IEnumerable<Parameter> ExtractParameters(IEnumerable<MemberInfo> member)
        {
            return member.Select(info => new Parameter(info, info.GetCustomAttributes<AbstractMessageAttribute>(true)));
        }
    }
}
