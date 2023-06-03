using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.UI.Manager;
using Tolga.Scripts;


namespace Assets.Scripts.UI.StartMenu 
{
    public class StartMenu : MenuManager
    {
        
        public void ClickStart(string scene)
        {
            LoadScene(scene);
            AudioManager.Instance.PlayThemeSound("themeMusic");
            
            //MenuManager.OpenPanel(gameObject.transform.parent.)
        }
        public void ClickCredits()
        {
            
        }
        
    }

}