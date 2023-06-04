using Assets.Scripts.UI.Manager;
using Tolga.Scripts;
using UnityEngine;

namespace Assets.Scripts.UI.InGameMenu
{
    public class InGameMenu : MenuManager
    {
        public static InGameMenu Instance;

        private bool[] buttonsValue;

        // Start is called before the first frame update
        private void Start()
        {
            buttonsValue = new bool[3];
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        
        public void PointerDown(int index)
        {
            buttonsValue[index] = true;
        }

        public void PointerUp(int index)
        {
            buttonsValue[index] = false;
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            gameObject.SetActive(true); // Hide the UI canvas
            AudioManager.Instance.inGameAudioSource.Stop();
            AudioManager.Instance.PlayThemeSound("UI");
            
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false); // Show the UI canvas
            AudioManager.Instance.inGameAudioSource.Play();
            AudioManager.Instance.PlayThemeSound("themeMusic");
        }
          
        
    }
}