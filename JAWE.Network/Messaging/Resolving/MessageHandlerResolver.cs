using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Network.Messaging.Resolving
{
    internal static class MessageHandlerResolver
    {
        public static IEnumerable<MessageHandlerResolverResultSet> Resolve()
        {

            var result = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(type => typeof(IMessageHandler).IsAssignableFrom(type) && type.GetTypeInfo().GetCustomAttribute<MessageHandlerAttribute>() != null)
                .Select(type =>
                {
                    var messageAttribute = type.GetTypeInfo().GetCustomAttribute<MessageHandlerAttribute>();
                    return new MessageHandlerResolverResultSet(messageAttribute.MessageId, type);
                })
                .ToList();

            return result;
        }
    }
}
