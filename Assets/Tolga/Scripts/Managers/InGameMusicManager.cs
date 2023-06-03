namespace Tolga.Scripts.Managers
{
    using static AudioManager;
    public static partial class DisplayMusicInGame
    {
        public static void PlayPunishmentSound()
        {
            Instance.OnDebug("Played Punishment Sound");
            Instance.PlayInGameSound("Failed");

        }
        public static void PlayFinishedRoomSound()
        {
            Instance.OnDebug("Played Finished Room Sound");
            Instance.PlayInGameSound("FinishedRoom");
        }

        public static void PlayFinishedPartMusic()
        {
            Instance.OnDebug("Played Finished Part Sound");
            Instance.PlayInGameSound("PartFinished");
                
        }

        
    }    
}
