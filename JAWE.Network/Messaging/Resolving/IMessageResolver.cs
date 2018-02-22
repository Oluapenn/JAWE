using System.Collections.Generic;

namespace JAWE.Network.Messaging.Resolving
{
    public interface IMessageResolver
    {
        IEnumerable<ResolvedMessagesSet> Resolve();
    }
}
