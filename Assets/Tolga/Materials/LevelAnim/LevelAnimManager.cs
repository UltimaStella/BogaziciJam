using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tolga.Materials.LevelAnim
{
    public class LevelAnimManager : MonoBehaviour
    {
        public Animator animator;
        public void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void FadeToLevel ()
        {
            animator.SetBool("FadeOut",false);
        }
                
        public void LevelToFade ()
        {
            animator.SetBool("FadeOut",true);
        }

    }
}