using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.UI.Manager;

namespace Assets.Scripts.UI.InGameMenu
{

    public class InGameMenu : MenuManager
    {
        public enum buttons { gas, breaks, turbo };

        [SerializeField]
        GameObject car;



        bool[] buttonsValue;

        // Start is called before the first frame update
        void Start()
        {

            buttonsValue = new bool[3];
        }

        // Update is called once per frame
        public void Update()
        {
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
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }
    }
}