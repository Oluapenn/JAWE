namespace JAWE.Network.Codec
{
    public class WrMessageCodec : IMessageDecoder, IMessageEncoder
    {
        public byte DecodeKey { get; set; }
        public byte EncodeKey { get; set; }

        public WrMessageCodec(byte decodeKey, byte encodeKey)
        {
            DecodeKey = decodeKey;
            EncodeKey = encodeKey;
        }

        public byte[] Decode(byte[] input)
        {
            return XorByteArray(input, DecodeKey);
        }

        public byte[] Encode(byte[] input)
        {
            return XorByteArray(input, EncodeKey);
        }

        private static byte[] XorByteArray(byte[] input, byte xorValue)
        {
            for (var i = input.Length - 1; i >= 0; i--)
            {
                input[i] ^= xorValue;
            }

            return input;
        }
    }
}