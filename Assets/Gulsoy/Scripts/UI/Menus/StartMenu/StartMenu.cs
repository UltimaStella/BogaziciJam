using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.UI.Manager;


namespace Assets.Scripts.UI.StartMenu 
{
    public class StartMenu : MenuManager
    {

   


        public void ClickStart(string scene)
        {
            LoadScene("sad");
            //MenuManager.OpenPanel(gameObject.transform.parent.)
        }
        public void ClickCredits()
        {

        }

       
        
    }

}