using System;
using System.Collections.Generic;
using System.Reflection;

namespace JAWE.Network.Messaging.Processing
{
    public abstract class MessageHandler<TSender, TSessionFlags, TMessageType> : IMessageHandler
        where TSender : SessionClient<TSessionFlags>
        where TSessionFlags : struct, IConvertible
        where TMessageType : IMessage
    {
        private readonly List<TSessionFlags> _allowedFlags;
        private readonly List<TSessionFlags> _rejectedFlags;

        protected MessageHandler()
        {
            _allowedFlags = new List<TSessionFlags>();
            _rejectedFlags = new List<TSessionFlags>();
        }

        protected void Allow(params TSessionFlags[] flags)
        {
            SetFlags(_allowedFlags, flags);
        }

        protected void Reject(params TSessionFlags[] flags)
        {
            SetFlags(_rejectedFlags, flags);
        }

        private static void SetFlags(List<TSessionFlags> flagsList, IEnumerable<TSessionFlags> flags)
        {
            if (!typeof(TSessionFlags).GetTypeInfo().IsEnum)
            {
                throw new Exception("Rejected session flags need to be an enum.");
            }

            flagsList.AddRange(flags);
        }

        private bool FlagsPassed(TSender sender)
        {
            // Validate the current session against the rejected and allowed flags for this handler.
            // If a rejected flag has been found, then the packet will be dropped and the client disconnected.
            // If no allowed flags are set or if all allow flags have been found, then the packet will be processed.
            return !sender.HasAnyFlag(_rejectedFlags) && sender.HasAllFlags(_allowedFlags);
        }

        public bool Handle(Client abstractClient, IMessage message)
        {
            var sender = abstractClient as TSender;

            if (sender == null)
            {
                throw new Exception(string.Format("Invalid Sender for MessageHandler: {0}", GetType().Name));
            }

            return FlagsPassed(sender) && Process(sender, (TMessageType)message);
        }

        protected abstract bool Process(TSender sender, TMessageType message);
    }
}
