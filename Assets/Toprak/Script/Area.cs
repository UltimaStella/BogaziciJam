using System.Collections;
using UnityEngine;
using static Tolga.Scripts.Managers.DisplayMusicInGame;

public class Area : MonoBehaviour
{
    public int ID;
    [SerializeField] GameObject PlayerSpawnLocation;
    public int AreaTime;
    public int RemainTime { get; private set; }
    public int KilledEnemyCount = 0;
    public int ComboCount = 0;

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
        if (AreaTimerCoroutine == null) AreaTimerCoroutine = StartCoroutine(CountDown());
    }

    public void DeactivateArea()
    {
        if (AreaTimerCoroutine != null)
        {
            StopCoroutine(AreaTimerCoroutine);
            AreaTimerCoroutine = null;
        }
    }

    public void DeleteArea()
    {
        DeactivateArea();
        Destroy(gameObject);
    }

    public void PlayerFailed()
    {
        PlayPunishmentSound();
        MakeFallOnThemeSound("themeMusic");
        
        Player.Instance.Retry(PlayerSpawnLocation.transform.position);
        ResetArea();
        DeactivateArea();
        ActivateArea();
    }

    public void ResetArea()
    {
        Transform Enemies = transform.Find("Enemies");
        if (Enemies != null)
        {
            for (int i = 0; i < Enemies.childCount; i++)
                Enemies.GetChild(i).GetComponent<Enemy>().ResetEnemy();
            KilledEnemyCount = 0;
            ComboCount = 0;
        }
    }

    public int GetAreaTime() => AreaTime;
    public int GetRemainTime() => RemainTime;

}
