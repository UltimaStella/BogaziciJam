using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody RB;

    [SerializeField] float Acc;
    [SerializeField] float Dec;
    [SerializeField] float JumpPower;
    [SerializeField] float RotationSpeed;
    [SerializeField] float MaxSpeed;
    public static Player Instance { get; private set; }
    float Speed;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void Retry(Vector3 SpawnLoc)
    {
        transform.SetPositionAndRotation(SpawnLoc, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        float yMove = Input.GetAxis("Jump") * JumpPower;

        if (xMove != 0 || zMove != 0)
        {
            Vector3 movementDirection = new Vector3(xMove, 0, zMove);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.fixedDeltaTime);

            Speed = Speed >= MaxSpeed ? MaxSpeed : Speed + Acc;
        }
        else
        {
            Speed = Speed <= 0 ? 0 : Speed - Dec;
        }

        RB.velocity = Speed * Time.fixedDeltaTime * new Vector3(xMove, 0, zMove);
    }
}
