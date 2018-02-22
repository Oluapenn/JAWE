namespace JAWE.Network.Messaging
{
    public enum MessageId : ushort
    {
        LauncherInformation = 0x1010,
        Login = 0x1100,
        InitializeConnection = 0x1200,

        CloseGame = 0x6000,
        SerialGServ = 0x6100,
        JoinServer = 0x6200,
        Keepalive = 0x6400,

        SetChannel = 0x7001,
        RoomList = 0x7200,
    }
}