using System;
using System.IO;

namespace JAWE.Network.P2p
{
    public static class UdpMessageParser
    {
        public static T Parse<T>(BinaryReader reader) where T : IPeerMessage
        {
            try
            {
                var newInstance = Activator.CreateInstance<T>();

                if (newInstance != null)
                {
                    newInstance.Deserialize(reader);
                }

                return newInstance;
            }
            catch (Exception)
            {
                // ignored
            }

            return default(T);
        }
    }
}
