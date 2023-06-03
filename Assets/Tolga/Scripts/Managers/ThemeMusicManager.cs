namespace Tolga.Scripts.Managers
{
    using static AudioManager;
    
    public static partial class DisplayMusicInGame
    {
        public static void AddThemePitchSound(string sound)
        {
            Instance.themeAudioSource.pitch += Instance.pitchValue;
            Instance.OnDebug("Pitch Added");
        }
        
        public static void MakeFallOnThemeSound(string sound)
        {
            Instance.OnDebug("Fallen the sound");
            Instance.themeAudioSource.pitch = 1;
        }
        
    }    
}