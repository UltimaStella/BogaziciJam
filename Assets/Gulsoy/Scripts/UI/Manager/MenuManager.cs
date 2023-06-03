using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Manager
{
    public class MenuManager : MonoBehaviour
    {

        [SerializeField]
        public GameObject[] panels;

        [HideInInspector]
        public static void OpenPanel(GameObject panel) { panel.SetActive(true); }
        public static void ClosePanel(GameObject panel) { panel.SetActive(false); }
        public static void PopUpAds(GameObject panel) { panel.SetActive(false); }
        public void OpenSelectedPanel(GameObject selectedPanel)
        {
            foreach (GameObject panel in this.panels) ClosePanel(panel);

            OpenPanel(selectedPanel);
        }

        //public static void LoadScene(string sceneName)
        //{
        //    SceneManager.LoadScene(sceneName);
        //}
        public  void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName); // Wait until the asynchronous scene fully loads (it is for music)
        }
        
        public void ClickExit()
        {
            Application.Quit();
        }
    }
}
