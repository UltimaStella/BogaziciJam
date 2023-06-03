using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    public Slider slider;
    public float volumeToSet;   
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = volumeToSet;
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume();
        Debug.Log(AudioListener.volume);
    }

    public void SetVolume()
    {
        AudioListener.volume = slider.value; 
    }

}

