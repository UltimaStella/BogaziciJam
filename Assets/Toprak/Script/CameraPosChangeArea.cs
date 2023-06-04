using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosChangeArea : MonoBehaviour
{
    [SerializeField] Transform CameraHolder;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Vector3 newOffset;

    public bool isPlayerInArea;

    Vector3 initOffset;
    Vector3 lastOffset;

    bool hasInitOffset = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = true;
            CameraHolder.GetComponent<CameraMovement>().enabled = false;
            if (!hasInitOffset) 
            { 
                initOffset = CameraHolder.GetComponent<CameraMovement>().Offset; 
                hasInitOffset = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = false;
            CameraHolder.GetComponent<CameraMovement>().UpdateOffset();
            CameraHolder.GetComponent<CameraMovement>().enabled = true;
            lastOffset = CameraHolder.GetComponent<CameraMovement>().Offset;
            if (Vector3.Distance(startPoint.position, Player.Instance.transform.position)
                <
                Vector3.Distance(endPoint.position, Player.Instance.transform.position))
            {
                StartCoroutine(ToInitOffset());
            }
            else
            {
                StartCoroutine(ToNewOffset());
            }
        }
    }

    IEnumerator ToNewOffset()
    {
        while (Vector3.Distance(newOffset, lastOffset) > 0.01)
        {
            lastOffset = Vector3.Lerp(lastOffset, newOffset, Time.deltaTime);
            CameraHolder.GetComponent<CameraMovement>().Offset = lastOffset;
            yield return null;
        }
    }

    IEnumerator ToInitOffset()
    {
        while (Vector3.Distance(initOffset, lastOffset) > 0.01)
        {
            lastOffset = Vector3.Lerp(lastOffset, initOffset, Time.deltaTime);
            CameraHolder.GetComponent<CameraMovement>().Offset = lastOffset;
            yield return null;
        }
    }
}
