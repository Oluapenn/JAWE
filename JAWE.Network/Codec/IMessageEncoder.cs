namespace JAWE.Network.Codec
{
    public interface IMessageEncoder
    {
        byte[] Encode(byte[] input);
    }
}