using System.Collections;
using UnityEngine;
using static Tolga.Scripts.Managers.DisplayMusicInGame;

public class Area : MonoBehaviour
{
    public int ID;
    [SerializeField] Transform PlayerSpawnLocation;
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

        StartCoroutine(MovePlayerToSpawnPoint());
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
 
    IEnumerator MovePlayerToSpawnPoint()
    {
        Player.Instance.GetComponent<Player>().enabled = false;
        while (Vector3.Distance(Player.Instance.transform.position, PlayerSpawnLocation.position) > 1f)
        {
            Player.Instance.GetComponent<Collider>().isTrigger = true;
            Player.Instance.transform.position = Vector3.Lerp(Player.Instance.transform.position, PlayerSpawnLocation.position, Time.deltaTime);
            Player.Instance.transform.LookAt(new Vector3(PlayerSpawnLocation.position.x, Player.Instance.transform.position.y, PlayerSpawnLocation.position.z));
            Player.Instance.animator.SetBool("Walking", true);
            Player.Instance.animator.SetBool("Running", true);
            yield return null;
        }

        Player.Instance.GetComponent<Collider>().isTrigger = false;
        Player.Instance.GetComponent<Player>().enabled = true;
        ResetArea();
        DeactivateArea();
        ActivateArea();
    } 
}
