using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messaging.Resolving
{
    public class WrMessageResolver : IMessageResolver
    {
        public IEnumerable<ResolvedMessagesSet> Resolve()
        {
            var result = typeof(WrMessageResolver)
                .GetTypeInfo()
                .Assembly
                .GetTypes()
                .Where(type => typeof(IMessage).IsAssignableFrom(type) && type.GetTypeInfo().GetCustomAttribute<MessageAttribute>() != null)
                .Select(type =>
                {
                    var messageAttribute = type.GetTypeInfo().GetCustomAttribute<MessageAttribute>();
                    return new ResolvedMessagesSet(type, messageAttribute);
                })
                .ToList();

            return result;
        }
    }
}
