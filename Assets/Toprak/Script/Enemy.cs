using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Speed = 10;
    [SerializeField] float RotationSpeed = 3;
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
            Vector3 playerPos = Player.Instance.transform.position; playerPos.y = initPos.y;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
            Vector3 _direction = (playerPos - transform.position).normalized;
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            area.KilledEnemyCount++;
            if (Player.Instance.inDash) area.ComboCount++;
            else Player.Instance.MovementPenalty();
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
