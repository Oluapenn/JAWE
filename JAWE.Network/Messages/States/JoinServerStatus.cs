namespace JAWE.Network.Messages.States
{
    public enum JoinServerStatus
    {
        Success = 1,
        NormalProcedure = 73030,
        //ErrorLoadingAccount = 73050,
        InvalidPacket = 90100, // Invalid Packet.
        UnregisteredUser = 90101, // Unregistered User.
        Min6Chars = 90102, // You must type at least 6 characters . --> Something with pcBang  = internet cafe)
        Min6CharNickname = 90103, // Nickname should be at least 6 charaters.
        IdInUseOtherServer = 90104, // Same ID is being used on the server.
        Inaccessible = 90105, //  Server is not accessible.
        TrainingServer = 90106, // Trainee server is accesible until the rank of a private..
        ClanWarError = 90112, // You cannot participate in Clan War
        Unused1 = 90115, // m135_1 [NOT USED]  = Something nickname related?)
        Unused2 = 90113, // m1000_1 [NOT USED]
        NoResponse = 91010, // Connection terminated because of lack of response for a while.
        ServerFull = 91020, // You cannot connect. Server is full.
        SomethingIsBussy = 91030, // Info request are in traffic.
        AccountFailedUpdate = 91040, // Account update has failed.
        SyncError = 91050, // User Info synchronization has failed.
        IdInUse = 92040, // That ID is currently being used.

        CafeIpLimit = 92050,
        // IF sc->BillOption
        // Disconnecting because number of IP's have exceeded.
        // ELSE
        // WR is a paid Internet Cafe game. You can play only at registered Internet Cafes.

        CafeTimeExpired = 92060, // Connection terminated because Internet Caf? time is over. Please check with staff.
        CannotConnect = 93010, // cannot connect to server.
        PremiumOnly = 98010, // Available to Premium users only.
    }
}
