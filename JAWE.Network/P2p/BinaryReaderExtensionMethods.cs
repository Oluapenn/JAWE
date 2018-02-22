using System.IO;
using System.Net;

namespace JAWE.Network.P2p
{
    internal static class BinaryReaderExtensionMethods
    {
        public static ushort ReadUInt16BE(this BinaryReader br)
        {
            return (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());
        }

        public static uint ReadUInt32BE(this BinaryReader br)
        {
            return (uint)IPAddress.NetworkToHostOrder(br.ReadInt32());
        }

        public static ulong ReadUInt64BE(this BinaryReader br)
        {
            return (ulong)IPAddress.NetworkToHostOrder(br.ReadInt64());
        }

        public static short ReadInt16BE(this BinaryReader br)
        {
            return IPAddress.NetworkToHostOrder(br.ReadInt16());
        }

        public static int ReadInt32BE(this BinaryReader br)
        {
            return IPAddress.NetworkToHostOrder(br.ReadInt32());
        }

        public static long ReadInt64BE(this BinaryReader br)
        {
            return IPAddress.NetworkToHostOrder(br.ReadInt64());
        }
    }
}
