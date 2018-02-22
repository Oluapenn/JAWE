using System;
using System.Collections.Generic;
using System.Linq;

namespace JAWE.Network
{
    public abstract class SessionClient<TSessionFlags> : Client
        where TSessionFlags : struct, IConvertible
    {
        private List<TSessionFlags> _sessionFlags;
        
        protected SessionClient()
        {
            _sessionFlags = new List<TSessionFlags>();
        }

        #region Session Flags

        public bool HasFlag(TSessionFlags flag)
        {
            return _sessionFlags.Contains(flag);
        }

        public bool HasAnyFlag(List<TSessionFlags> flags)
        {
            return flags.Intersect(_sessionFlags).Any();
        }

        public bool HasAllFlags(List<TSessionFlags> flags)
        {
            return flags.Intersect(_sessionFlags).Count() == flags.Count;
        }

        public void AddFlag(params TSessionFlags[] flags)
        {
            foreach (var flag in flags)
            {
                if (!HasFlag(flag))
                {
                    _sessionFlags.Add(flag);
                }
            }
        }

        public void RemoveFlag(params TSessionFlags[] flags)
        {
            _sessionFlags = _sessionFlags.Except(flags).ToList();
        }

        #endregion

    }
}
