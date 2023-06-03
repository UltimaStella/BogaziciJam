using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Speed = 10;
    [SerializeField] float StopDistance;

    Vector3 initPos;
    Quaternion initQuartenion;

    Area area;

    void Start()
    {
        area = transform.parent.parent.GetComponent<Area>();
        initPos = transform.localPosition;
        initQuartenion = transform.localRotation;
    }
    
    void Update()
    {
        if (AreaManager.Instance.CurrentArea == area && Vector3.Distance(transform.position, Player.Instance.transform.position) > StopDistance)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, step);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            area.KilledEnemyCount++;
            gameObject.SetActive(false);
        }
    }

    public void ResetEnemy()
    {
        transform.localPosition = initPos;
        transform.localRotation = initQuartenion;
        gameObject.SetActive(true);
    }
}
