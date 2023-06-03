using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace Tolga.Scripts
{
    [System.Serializable]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [Header("To see Audio Debug")]
        public bool onDebugMode = false;
        
        [Header("Theme Game Music")]
        public AudioSource themeAudioSource;
        
        [Header("General Junk Game Music")]
        public  AudioSource inGameAudioSource;
        public ClipManager[] inGameClipManagers;

        [Header("Used Sound Name In Game")]
        public readonly string[] SoundNamesInCode =new []{"themeMusic","FinishedRoom","PartFinished","Failed"};
        
        [Header("To Make It Faster")]
        [Range(-3,3)]
        public float pitchValue;
        
        
        private void Start()
        {
            themeAudioSource = GetComponent<AudioSource>();
            if(themeAudioSource == null) Debug.Log("Theme Audio Source is Null!");
            if(inGameAudioSource == null) Debug.Log("Game Audio Source is Null!");
            else if(inGameClipManagers.Length == 0) Debug.Log("Empty AudioClip");
            
            
            PlaySound("themeMusic");
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void PlaySound(string sound)
        {
            foreach (var manager in inGameClipManagers)
            {
                if (sound != manager.name) continue;

                if (sound == "themeMusic")
                {
                    themeAudioSource.clip = manager.clip;
                    themeAudioSource.Play();
                    continue;
                }
                
                inGameAudioSource.clip = manager.clip;
                inGameAudioSource.Play();
            }
            
        }

        public void AddThemePitchSound(string sound)
        {
            themeAudioSource.pitch += pitchValue;
            OnDebug("Pitch Added");

        }

        public void MakeFallOnThemeSound(string sound)
        {
            OnDebug("Fallen the sound");
            themeAudioSource.pitch = 1;
        }

        public void PlayPunishmentSound()
        {
            OnDebug("Played Punishment Sound");
            PlaySound("Failed");

        }
        
        public void PlayFinishedRoomSound()
        {
            OnDebug("Played Finished Room Sound");
            PlaySound("FinishedRoom");
        }

        public void PlayFinishedPartMusic()
        {
            OnDebug("Played Finished Part Sound");
            PlaySound("PartFinished");
            
        }

        public void OnDebug(in string @string)
        {
            if (onDebugMode)
                UnityEngine.Debug.Log(@string);
        }
        
    }
    
}
[System.Serializable] 
public class ClipManager
{
    [SerializeField] public string name;
    [SerializeField] public AudioClip clip;
    
     
}
 