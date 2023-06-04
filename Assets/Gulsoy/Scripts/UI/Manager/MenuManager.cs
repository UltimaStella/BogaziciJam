using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Manager
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] public GameObject[] panels;

        [HideInInspector]
        public static void OpenPanel(GameObject panel)
        {
            panel.SetActive(true);
        }

        public void ClosePanel(GameObject panel)
        {
            panel.SetActive(false);
        }

        public static void PopUpAds(GameObject panel)
        {
            panel.SetActive(false);
        }

        public void OpenSelectedPanel(GameObject selectedPanel)
        {
            foreach (var panel in panels) ClosePanel(panel);

            OpenPanel(selectedPanel);
        }
        public void LoadScene(string sceneName)
        {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync(sceneName); // Wait until the asynchronous scene fully loads (it is for music)
        }

        public void ClickExit()
        {
            Time.timeScale = 1f;
            Application.Quit();
        }
    }
}