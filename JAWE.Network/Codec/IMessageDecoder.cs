namespace JAWE.Network.Codec
{
    public interface IMessageDecoder
    {
        byte[] Decode(byte[] input);
    }
}