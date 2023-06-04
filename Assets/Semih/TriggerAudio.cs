using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    FMODUnity.EventReference sound;
    public string Event;
    public bool PlayOnAwake;

    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
    }

    public void Start()
    {
        if (PlayOnAwake)
        {
            PlayOneShot();
        }
    }
}
