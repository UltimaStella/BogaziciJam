using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosChangeArea : MonoBehaviour
{
    [SerializeField] Transform CameraHolder;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

    public bool isPlayerInArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = true;
            CameraHolder.GetComponent<CameraMovement>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = false;
            CameraHolder.GetComponent<CameraMovement>().UpdateOffset();
            CameraHolder.GetComponent<CameraMovement>().enabled = true;
        }
    }
}
