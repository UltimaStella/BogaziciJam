using UnityEngine;
using System.Collections;
using Tolga.Scripts;

namespace SlimUI.ModernMenu{
	public class CheckMusicVolume : MonoBehaviour {
		public void  Start (){
			// remember volume level from last time
			foreach (var ui in AudioManager.Instance.themeClipManagers)
			{
				if(ui.name != "UI") continue;

				GetComponent<AudioSource>().clip = ui.clip;
				GetComponent<AudioSource>().Play();

			}
			
			GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
		}

		public void UpdateVolume (){
			GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
		}
	}
}