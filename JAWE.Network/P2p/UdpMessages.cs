namespace JAWE.Network.P2p
{
    public enum UdpMessages : ushort
    {
        PeerAddr = 0x2100,
        Keepalive = 0x3000,
        MsnPlayerLogin = 0x4200,
        MsnPlayerLogout = 0x4201,
        MsnWake = 0x4210,
        MsnChat = 0x4220,
        MsFriendMng = 0x7E00,
        CharMove = 0x3100,
        CharMoveA = 0x3104,
        CharMoveB = 0x3105,
        CharRoll = 0x3101,
        CharZoomin = 0x3102,
        CharEmotion = 0x3103,
        ObjMove = 0x3200,
        VehicleMove = 0x3201,
        VehicleMoveA = 0x3202,
        VehicleMoveB = 0x3203,
        Bullet = 0x3400,
        Explosion = 0x3403,
        DummyInfo = 0x3401,
        HackInfo = 0x3402,
        TextChat = 0x3600,
    }
}
