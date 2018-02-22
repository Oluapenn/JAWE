using System;
using System.IO;
using System.Net;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.P2p.Messages
{
    public class MessageHeader : IPeerMessage
    {
        private static readonly Type IdentityType = typeof(UdpHeaderType);

        #region Parameters

        [Parameter(0)]
        public UdpHeaderType Identity { get; set; }

        [Parameter(1)]
        public byte BAck { get; set; }

        [Parameter(2)]
        public byte BCmp { get; set; }

        [Parameter(3)]
        public ushort SerialNumber { get; set; }

        [Parameter(4)]
        public ushort RoomNumber { get; set; }

        [Parameter(5)]
        public ushort Session { get; set; }

        [Parameter(6)]
        public uint SeedCode { get; set; }

        #endregion

        #region Serialization

        public void Deserialize(BinaryReader reader)
        {
            var identity = reader.ReadUInt16BE();

            if (!Enum.IsDefined(IdentityType, identity))
                throw new Exception($"Unknown UDP Message Identity: {identity}.");

            Identity = (UdpHeaderType) identity;
            BAck = reader.ReadByte();
            BCmp = reader.ReadByte();
            SerialNumber = reader.ReadUInt16BE();
            RoomNumber = reader.ReadUInt16BE();
            Session = reader.ReadUInt16BE();
            SeedCode = reader.ReadUInt32BE();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((ushort)IPAddress.HostToNetworkOrder((ushort)Identity));
            writer.Write(BAck);
            writer.Write(BCmp);
            writer.Write((ushort)IPAddress.HostToNetworkOrder(SerialNumber));
            writer.Write((ushort)IPAddress.HostToNetworkOrder(RoomNumber));
            writer.Write((ushort)IPAddress.HostToNetworkOrder(Session));
            writer.Write((uint)IPAddress.HostToNetworkOrder(SeedCode));
        }

        #endregion

        public override string ToString()
        {
            return $"MessageHeader(Identity: {Identity}, BAck: {BAck}, BCmp: {BCmp}, SerialNumber: {SerialNumber}, RoomNumber: {RoomNumber}, Session: {Session}, SeedCode: {SeedCode})";
        }
    }
}
