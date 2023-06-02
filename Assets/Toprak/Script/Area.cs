using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public int ID;
    [SerializeField] GameObject PlayerStartLocation;
    [SerializeField] float AreaTime;
    float RemainTime;

    Coroutine AreaTimerCoroutine;

    IEnumerator CountDown()
    {
        while (RemainTime > 0)
        {
            RemainTime = AreaTime;
            yield return new WaitForSeconds(1);
        }
        PlayerFailed();
    }

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void MakeVisible()
    {
        gameObject.SetActive(true);
    }

    public void ActivateArea()
    {
        AreaTimerCoroutine = StartCoroutine("CountDown");
    }

    public void DeactivateArea() 
    { 
        Destroy(gameObject);
        StopCoroutine(AreaTimerCoroutine);
    }

    public void PlayerFailed()
    {

    }
}
