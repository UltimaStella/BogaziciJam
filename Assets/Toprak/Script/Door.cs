using UnityEngine;
using static Tolga.Scripts.Managers.DisplayMusicInGame;

public class Door : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AreaManager.Instance.GoToNextArea();

            PlayFinishedRoomSound();
        }
    }

    void OnTriggerExit(Collider other) { GetComponent<Collider>().isTrigger = false; }
}
