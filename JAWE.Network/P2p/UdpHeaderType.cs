namespace JAWE.Network.P2p
{
    public enum UdpHeaderType : ushort
    {
        Ping = 0x1000,
        Connect = 0x1001,
        Disconnect = 0x1002,
        Echo = 0x1003, // 4099
        Authentication = 0x1004,
        Broadcast = 0x1005,
        Received = 0x1006,
        Descript = 0x1010,
        P2PDescript = 0x3000
    }
}
