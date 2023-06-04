using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody RB;

    [SerializeField] float Acc;
    [SerializeField] float Dec;
    [SerializeField] float JumpForce;
    [SerializeField] float RotationSpeed;
    [SerializeField] float MaxMovementSpeed;
    [SerializeField] float FallMultiplier;

    public static Player Instance { get; private set; }
    float Speed;

    float gravity = 9.8f;
    bool isGrounded = true;
    float VerticalSpeed;

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

        if (xMove != 0 || zMove != 0)
        {
            Vector3 movementDirection = new Vector3(xMove, 0, zMove);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.fixedDeltaTime);

            Speed = Speed >= MaxMovementSpeed ? MaxMovementSpeed : Speed + Acc;
        }
        else Speed = Speed <= 0 ? 0 : Speed - Dec;

        Vector3 movement = Speed * Time.fixedDeltaTime * new Vector3(xMove, 0, zMove);

        VerticalSpeed = isGrounded ? 0 : VerticalSpeed - Time.fixedDeltaTime * gravity * FallMultiplier;
        Vector3 vertical = Time.fixedDeltaTime * VerticalSpeed * transform.up;
        movement += vertical;
        RB.velocity = movement;
    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.E)) 
        { 
            VerticalSpeed = JumpForce;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;
    }
}
