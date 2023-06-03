using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace Tolga.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        private AudioSource _audioSource;
        public ClipManager[] clipManagers;
        
        [Header("To Make It Faster")]
        [Range(-3,3)]
        public float pitchValue;
        
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if(_audioSource == null) Debug.Log("Audio Source is Null!");
            else if(clipManagers.Length == 0) Debug.Log("Empty AudioClip");

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
            foreach (var manager in clipManagers)
            {
                if (sound != manager.name) continue;
                
                _audioSource.clip = manager.clip;
                _audioSource.Play();
            }
            
        }

        public void AddPitchSound(string sound)
        {
            _audioSource.pitch += pitchValue;
            Debug.Log("Pitch Added");
        }

        public void FallSound(string sound)
        {
            Debug.Log("Fallen the sound");
            _audioSource.pitch = 1;
        }

        public void PlayPunishmentSound()
        {
            Debug.Log("Played Punishment Sound");

        }
        public void PlayFinishedRoomSound()
        {
            Debug.Log("Played Finished Room Sound");
        }

        public void PlayFinishedPartMusic()
        {
            Debug.Log("Played Finished Part Sound");

        }

        public void PlayDieMusic()
        {
            Debug.Log("Played Die Sound");
            

        }
        
        
    }
    
}
[System.Serializable] 
public class ClipManager
{
    [SerializeField] public string name;
    [SerializeField] public AudioClip clip;
    
     
}