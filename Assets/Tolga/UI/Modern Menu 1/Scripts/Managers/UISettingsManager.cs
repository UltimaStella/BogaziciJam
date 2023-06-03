using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace SlimUI.ModernMenu{
	public class UISettingsManager : MonoBehaviour {

		public enum Platform {Desktop, Mobile};
		public Platform platform;
		[Header("CONTROLS SETTINGS")]

		public GameObject musicSlider;

		private float sliderValue = 0.0f;
		private float sliderValueXSensitivity = 0.0f;
		private float sliderValueYSensitivity = 0.0f;
		private float sliderValueSmoothing = 0.0f;
		

		public void  Start (){ 
			// check slider values
			musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
 

			// check shadow distance/enabled
			if(platform == Platform.Desktop){
				if(PlayerPrefs.GetInt("Shadows") == 0){
					QualitySettings.shadowCascades = 0;
					QualitySettings.shadowDistance = 0; 
				}
				else if(PlayerPrefs.GetInt("Shadows") == 1){
					QualitySettings.shadowCascades = 2;
					QualitySettings.shadowDistance = 75; 
				}
				else if(PlayerPrefs.GetInt("Shadows") == 2){
					QualitySettings.shadowCascades = 4;
					QualitySettings.shadowDistance = 500; 
				}
			}else if(platform == Platform.Mobile){
				if(PlayerPrefs.GetInt("MobileShadows") == 0){
					QualitySettings.shadowCascades = 0;
					QualitySettings.shadowDistance = 0; 
				}
				else if(PlayerPrefs.GetInt("MobileShadows") == 1){
					QualitySettings.shadowCascades = 2;
					QualitySettings.shadowDistance = 75; 
				}
				else if(PlayerPrefs.GetInt("MobileShadows") == 2){
					QualitySettings.shadowCascades = 4;
					QualitySettings.shadowDistance = 100; 
				}
			}
  
 
			// check texture quality
			if(PlayerPrefs.GetInt("Textures") == 0){
				QualitySettings.masterTextureLimit = 2; 
			}
			else if(PlayerPrefs.GetInt("Textures") == 1){
				QualitySettings.masterTextureLimit = 1; 
			}
			else if(PlayerPrefs.GetInt("Textures") == 2){
				QualitySettings.masterTextureLimit = 0; 
			}
		}

		public void Update (){
			//sliderValue = musicSlider.GetComponent<Slider>().value;
		}

		public void FullScreen (){
			Screen.fullScreen = !Screen.fullScreen;
 
		}

		public void MusicSlider (){
			//PlayerPrefs.SetFloat("MusicVolume", sliderValue);
			PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);
		}

		public void SensitivityXSlider (){
			PlayerPrefs.SetFloat("XSensitivity", sliderValueXSensitivity);
		}

		public void SensitivityYSlider (){
			PlayerPrefs.SetFloat("YSensitivity", sliderValueYSensitivity);
		}

		public void SensitivitySmoothing (){
			PlayerPrefs.SetFloat("MouseSmoothing", sliderValueSmoothing);
			Debug.Log(PlayerPrefs.GetFloat("MouseSmoothing"));
		}

		// the playerprefs variable that is checked to enable hud while in game
		public void ShowHUD (){
			if(PlayerPrefs.GetInt("ShowHUD")==0){
				PlayerPrefs.SetInt("ShowHUD",1); 
			}
			else if(PlayerPrefs.GetInt("ShowHUD")==1){
				PlayerPrefs.SetInt("ShowHUD",0); 
			}
		}

		// the playerprefs variable that is checked to enable mobile sfx while in game
		public void MobileSFXMute (){
			if(PlayerPrefs.GetInt("Mobile_MuteSfx")==0){
				PlayerPrefs.SetInt("Mobile_MuteSfx",1); 
			}
			else if(PlayerPrefs.GetInt("Mobile_MuteSfx")==1){
				PlayerPrefs.SetInt("Mobile_MuteSfx",0); 
			}
		}

		public void MobileMusicMute (){
			if(PlayerPrefs.GetInt("Mobile_MuteMusic")==0){
				PlayerPrefs.SetInt("Mobile_MuteMusic",1); 
			}
			else if(PlayerPrefs.GetInt("Mobile_MuteMusic")==1){
				PlayerPrefs.SetInt("Mobile_MuteMusic",0); 
			}
		}

		// show tool tips like: 'How to Play' control pop ups
		public void ToolTips (){
			if(PlayerPrefs.GetInt("ToolTips")==0){
				PlayerPrefs.SetInt("ToolTips",1); 
			}
			else if(PlayerPrefs.GetInt("ToolTips")==1){
				PlayerPrefs.SetInt("ToolTips",0); 
			}
		}

		public void NormalDifficulty (){ 
			PlayerPrefs.SetInt("NormalDifficulty",1);
			PlayerPrefs.SetInt("HardCoreDifficulty",0);
		}

		public void HardcoreDifficulty (){ 
			PlayerPrefs.SetInt("NormalDifficulty",0);
			PlayerPrefs.SetInt("HardCoreDifficulty",1);
		}

		public void ShadowsOff (){
			PlayerPrefs.SetInt("Shadows",0);
			QualitySettings.shadowCascades = 0;
			QualitySettings.shadowDistance = 0; 
		}

		public void ShadowsLow (){
			PlayerPrefs.SetInt("Shadows",1);
			QualitySettings.shadowCascades = 2;
			QualitySettings.shadowDistance = 75; 
		}

		public void ShadowsHigh (){
			PlayerPrefs.SetInt("Shadows",2);
			QualitySettings.shadowCascades = 4;
			QualitySettings.shadowDistance = 500; 
		}

		public void MobileShadowsOff (){
			PlayerPrefs.SetInt("MobileShadows",0);
			QualitySettings.shadowCascades = 0;
			QualitySettings.shadowDistance = 0; 
		}

		public void MobileShadowsLow (){
			PlayerPrefs.SetInt("MobileShadows",1);
			QualitySettings.shadowCascades = 2;
			QualitySettings.shadowDistance = 75; 
		}

		public void MobileShadowsHigh (){
			PlayerPrefs.SetInt("MobileShadows",2);
			QualitySettings.shadowCascades = 4;
			QualitySettings.shadowDistance = 500; 
		}

		public void vsync (){
			if(QualitySettings.vSyncCount == 0){
				QualitySettings.vSyncCount = 1; 
			}
			else if(QualitySettings.vSyncCount == 1){
				QualitySettings.vSyncCount = 0; 
			}
		}

		public void InvertMouse (){
			if(PlayerPrefs.GetInt("Inverted")==0){
				PlayerPrefs.SetInt("Inverted",1);
			}
			else if(PlayerPrefs.GetInt("Inverted")==1){
				PlayerPrefs.SetInt("Inverted",0);
			}
		}

		public void MotionBlur (){
			if(PlayerPrefs.GetInt("MotionBlur")==0){
				PlayerPrefs.SetInt("MotionBlur",1); 
			}
			else if(PlayerPrefs.GetInt("MotionBlur")==1){
				PlayerPrefs.SetInt("MotionBlur",0); 
			}
		}

		public void AmbientOcclusion (){
			if(PlayerPrefs.GetInt("AmbientOcclusion")==0){
				PlayerPrefs.SetInt("AmbientOcclusion",1); 
			}
			else if(PlayerPrefs.GetInt("AmbientOcclusion")==1){
				PlayerPrefs.SetInt("AmbientOcclusion",0); 
			}
		}

		public void CameraEffects (){
			if(PlayerPrefs.GetInt("CameraEffects")==0){
				PlayerPrefs.SetInt("CameraEffects",1);
				 
			}
			else if(PlayerPrefs.GetInt("CameraEffects")==1){
				PlayerPrefs.SetInt("CameraEffects",0); 
			}
		}

		public void TexturesLow (){
			PlayerPrefs.SetInt("Textures",0);
			QualitySettings.masterTextureLimit = 2; 
		}

		public void TexturesMed (){
			PlayerPrefs.SetInt("Textures",1);
			QualitySettings.masterTextureLimit = 1; 
		}

		public void TexturesHigh (){
			PlayerPrefs.SetInt("Textures",2);
			QualitySettings.masterTextureLimit = 0; 
		}
	}
}