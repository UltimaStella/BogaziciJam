using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    public Slider slider;

    public float volumeToSet;

    // Start is called before the first frame update
    private void Start()
    {
        AudioListener.volume = volumeToSet;
    }

    // Update is called once per frame
    private void Update()
    {
        SetVolume();
        Debug.Log(AudioListener.volume);
    }

    public void SetVolume()
    {
        AudioListener.volume = slider.value;
    }
}