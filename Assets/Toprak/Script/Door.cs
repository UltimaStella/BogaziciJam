using Tolga.Scripts;
using UnityEngine;

public class Door : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AreaManager.Instance.GoToNextArea();
            AudioManager.Instance.AddPitchSound("themeMusic");
        }
    }

    void OnTriggerExit(Collider other) { GetComponent<Collider>().isTrigger = false; }
}
