namespace JAWE.Network.Messages.States
{
    public enum BattleMode : byte
    {
        Explosive = 0,
        FreeForAll = 1,
        Deathmatch = 2,
        Conquest = 3,
        LargeMission = 4,
        SmallMission = 5,
        SmallHero = 6,
        Survival = 7,
        Defence = 8,
        DefencePoint = 9,
        Training = 10

        // below is extracted from original source -> pdb
        /*
        CS = 0,
        PersonalDeathmatch = 1,
        Deathmatch = 2,
        Conquest = 3,
        LargeMission = 4,
        SmallMission = 5,
        SmallHero = 6,
        Survival = 7,
        Defence = 8,
        DefencePoint = 9,
        Training = 10
        */
    }
}
