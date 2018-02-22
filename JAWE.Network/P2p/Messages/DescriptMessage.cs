using System;
using System.IO;

namespace JAWE.Network.P2p.Messages
{
    public class DescriptMessage : IPeerMessage
    {
        private static readonly Type MessageType = typeof(UdpMessages);

        #region Parameters

        public UdpMessages Command { get; set; }

        public ushort Length { get; set; }

        public byte EntireNum { get; set; }

        public byte Sequence { get; set; }
        
        public byte BUseRelay { get; set; }

        public byte Flag { get; set; }

        public ushort ToSerial { get; set; }

        public ushort ToRoomNumber { get; set; }

        public ushort ToSession { get; set; }

        #endregion

        #region Serialization

        public void Deserialize(BinaryReader reader)
        {
            var commandId = reader.ReadUInt16BE();

            if (!Enum.IsDefined(MessageType, commandId))
                throw new Exception($"Unknown Descript Message CommandId: {commandId}.");

            Command = (UdpMessages)commandId;
            Length = reader.ReadUInt16BE();
            EntireNum = reader.ReadByte();
            Sequence = reader.ReadByte();
            BUseRelay = reader.ReadByte();
            Flag = reader.ReadByte();
            ToSerial = reader.ReadUInt16BE();
            ToRoomNumber = reader.ReadUInt16BE();
            ToSession = reader.ReadUInt16BE();
        }

        public void Serialize(BinaryWriter writer)
        {

        }

        #endregion

        public override string ToString()
        {
            return $"DescriptMessage(CommandId: {Command}, Length: {Length}, EntireNum: {EntireNum}, Sequence: {Sequence}, BUseRelay: {BUseRelay}, Flag: {Flag}, ToSerial: {ToSerial}, ToRoomNumber: {ToRoomNumber}, ToSession: {ToSession})";
        }
    }
}
