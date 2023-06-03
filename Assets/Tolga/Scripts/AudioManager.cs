using System;
using System.Linq;
using JetBrains.Annotations;
using Tolga.Scripts;
using Tolga.Scripts.Managers;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Tolga.Scripts
{
    using static DisplayMusicInGame;
    [System.Serializable]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("To see Audio Debug")]
        public bool onDebugMode = false;
        public bool onSceneDebugMode = false;
        
        [Header("Theme Game Music")] public AudioSource themeAudioSource;
        
        [Header("General Junk Game Music")]
        public  AudioSource inGameAudioSource;
        public ClipManager[] inGameClipManagers;
        public ClipManager[] themeClipManagers;
        
        [Header("Theme Game Music")] 
        public AudioClip buttonClip;
      
        [Header("To Make Theme Music Faster")]
        [Range(-3,3)]
        public float pitchValue;
        
//        [Header("Used Sound Name In Game")] // unused
//        public readonly string[] SoundNamesInGameClip =new []{"FinishedRoom","PartFinished","Failed"};
//        public readonly string[] SoundNamesInThemeClip =new []{"themeMusic","UI"};

        private void Start()
        {
            if(themeAudioSource == null) Debug.Log("Theme Audio Source is Null!");
            if(inGameAudioSource == null) Debug.Log("Game Audio Source is Null!");
            else if(inGameClipManagers.Length == 0) Debug.Log("Empty AudioClip");

            if (onSceneDebugMode)
            {
                PlayThemeSound(SceneManager.GetActiveScene().buildIndex == 1 ? "themeMusic" : "UI");
            }
            
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        public void PlayThemeSound(string sound)
        {
            foreach (var manager in themeClipManagers)
            {
                
                if (sound != manager.name) continue;
                OnDebug("found!");
                themeAudioSource.clip = manager.clip;
                themeAudioSource.Play();
            }

        }

        public void PlayInGameSound(string sound)
        {
            foreach (var manager in inGameClipManagers)
            {
                
                if (sound != manager.name) continue;

                inGameAudioSource.clip = manager.clip;
                inGameAudioSource.Play();
                
            }
            
        }        

        public void DisplayButtonSound()
        {
            inGameAudioSource.PlayOneShot(buttonClip,1f);
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
