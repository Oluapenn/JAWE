using System.IO;

namespace JAWE.Network.P2p
{
    public interface IPeerMessage
    {
        void Deserialize(BinaryReader reader);
        void Serialize(BinaryWriter writer);
    }
}
