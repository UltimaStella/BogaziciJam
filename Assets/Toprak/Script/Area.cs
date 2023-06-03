using System.Collections;
using System.Collections.Generic;
using Tolga.Scripts;
using UnityEngine;

public class Area : MonoBehaviour
{
    public int ID;
    [SerializeField] GameObject PlayerSpawnLocation;
    [SerializeField] int AreaTime;
    int RemainTime;

    Coroutine AreaTimerCoroutine;

    IEnumerator CountDown()
    {
        RemainTime = AreaTime;
        while (RemainTime > 0)
        {
            yield return new WaitForSeconds(1);
            --RemainTime;
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
        AreaTimerCoroutine = StartCoroutine(CountDown());
    }

    public void DeactivateArea()
    {
        if (AreaTimerCoroutine != null) StopCoroutine(AreaTimerCoroutine);
    }

    public void DeleteArea()
    {
        DeactivateArea();
        Destroy(gameObject);
    }

    
    public void PlayerFailed()
    {
        Debug.Log(PlayerSpawnLocation.transform.position);
        
        AudioManager.Instance.FallSound("themeMusic");
        
        Player.Instance.Retry(PlayerSpawnLocation.transform.position);
    }
}
