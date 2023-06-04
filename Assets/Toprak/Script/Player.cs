using System.Collections;
using UnityEngine;
using Assets.Scripts.UI.InGameMenu;

public class Player : MonoBehaviour
{
    Rigidbody RB;

    [SerializeField] float Acc;
    [SerializeField] float Dec;
    [SerializeField] float JumpForce;
    [SerializeField] float RotationSpeed;
    [SerializeField] float MaxWalkSpeed;
    [SerializeField] float MaxRunSpeed;
    [SerializeField] float FallMultiplier;
    [SerializeField] float DashSpeed;
    [SerializeField] float DashTime;
    [SerializeField] float DashCooldown;

    [SerializeField] float MovementSpeedPenaltyAmount;
    [SerializeField] float MovementSpeedPenaltyTime;
    public InGameMenu gameMenu;

    public static Player Instance { get; private set; }

    public Animator animator;
    bool isRunning = false;
    float Speed;

    float gravity = 9.8f;
    bool isGrounded = true;
    float VerticalSpeed;

    bool canDash = true;
    public bool inDash = false;

    float movability = 1; 

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
        if (inDash)
        {
            RB.velocity = transform.forward * DashSpeed;
        }
        else
        {
            float xMove = Input.GetAxis("Horizontal");
            float zMove = Input.GetAxis("Vertical");

            if (xMove != 0 || zMove != 0)
            {
                animator.SetBool("Walking", true);
                Vector3 movementDirection = new Vector3(xMove, 0, zMove);
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.fixedDeltaTime);

                if (isRunning) Speed = Speed >= MaxRunSpeed ? MaxRunSpeed : Speed + Acc;
                else Speed = Speed >= MaxWalkSpeed ? MaxWalkSpeed : Speed + Acc;
            }
            else  { Speed = Speed <= 0 ? 0 : Speed - Dec; animator.SetBool("Walking", false); }

            Vector3 movement = Speed * movability * Time.fixedDeltaTime * new Vector3(xMove, 0, zMove);

            VerticalSpeed = isGrounded ? 0 : VerticalSpeed - Time.fixedDeltaTime * gravity * FallMultiplier;
            Vector3 vertical = Time.fixedDeltaTime * VerticalSpeed * transform.up;
            movement += vertical;
            RB.velocity = movement;
        }
    }

    private void Update()
    {
        if (canDash && Input.GetKeyDown(KeyCode.Space)) StartCoroutine(Dash());
        if (inDash) return;

        if (isGrounded && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Jump");
            VerticalSpeed = JumpForce;
            isGrounded = false;
            animator.SetBool("Grounded", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) { isRunning = true; animator.SetBool("Running", true); }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {isRunning = false; animator.SetBool("Running", false);}
        if (Input.GetKeyUp(KeyCode.Escape)) { gameMenu.PauseGame(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) { isGrounded = true; animator.SetBool("Grounded",true); }
    }

    IEnumerator Dash()
    {
        animator.SetTrigger("Dash");
        canDash = false;
        inDash = true;
        yield return new WaitForSeconds(DashTime);
        inDash = false;
        Speed = 0;
        yield return new WaitForSeconds(DashCooldown);
        canDash = true;
    }

    public void MovementPenalty()
    {
        StartCoroutine(MovementSpeedPenalty());
    }

    IEnumerator MovementSpeedPenalty()
    {
        movability = MovementSpeedPenaltyAmount / 100;
        yield return new WaitForSeconds(MovementSpeedPenaltyTime);
        movability = 1;
    }
}
