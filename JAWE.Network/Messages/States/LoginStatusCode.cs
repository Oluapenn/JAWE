namespace JAWE.Network.Messages.States
{
    public enum LoginStatusCode
    {
        Success = 1,

        RequestNickname = 72000,
        UnregisteredUser = 72010,
        InvalidPassword = 72020,
        AlreadyLoggedIn = 72030,
        VerifyEmailAddress = 73040,
        BannedWithTime = 73020,
        NormalProcedure = 73030,
        Blocked = 73050,
        EnterId = 74010,
        EnterPassword = 74020,
        EnterCharacterName = 74030,
        ClientVersionMissmatch = 70301,
        FailedAccountValidation = 70201,
        AccountNotActive = 70202
    }
}
