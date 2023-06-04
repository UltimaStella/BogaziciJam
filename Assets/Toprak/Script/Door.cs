using UnityEngine;
using static Tolga.Scripts.Managers.DisplayMusicInGame;

public class Door : MonoBehaviour
{
    void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            AreaManager.Instance.GoToNextArea();

            PlayFinishedRoomSound();
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
