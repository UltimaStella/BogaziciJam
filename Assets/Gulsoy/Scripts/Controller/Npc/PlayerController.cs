using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float topSpeed;
    [SerializeField] private float bottomSpeed;
    [SerializeField] private float glideLimit;
    [SerializeField] private float jumpPower;
    [SerializeField] private float nerfTime = 20;
    [HideInInspector] private float speed;
    [HideInInspector] private float speedScale;
    [HideInInspector] private float audioSpeed;

    private float rotationAngle;

    int rotateRight;
    int rotateLeft;
    int rotateForward;
    int rotateBack;

    private Rigidbody rb;
    private bool canDash = true;
    private bool isDashing;
    private bool isGlading;
    [SerializeField] private float dashPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 1f;

    [SerializeField] private float gladingPower = 24f;
    private float glideTime = 0.1f;
    private float glideCoolDown = 1f;

    bool canJump;
    Vector3 movement;
    private int comboScore = 0;
    private bool q, w, e, a, s, d, shift, space, escape;


    // Start is called before the first frame update
    void Start()
    {
        audioSpeed = 1;
        speedScale = 1;
        rotationAngle = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
    }

    void Update()
    {
        GetPressedKeys();
        Move();
    }

    public int GetComboScore()
    {
        int tempScore = comboScore;
        comboScore = 0;
        return tempScore;
    }

    private void Move()
    {

        if (w) { movement += mainCamera.transform.forward; rotateForward = 1; }

        if (a) { movement -= mainCamera.transform.right; rotateLeft = 1; }

        if (d) { movement += mainCamera.transform.right; rotateRight = 1; }

        if (s) { movement -= mainCamera.transform.forward; rotateBack = 1; }

        if (shift) { speed = Mathf.Lerp(speed, topSpeed, 0.5f); }

        else { speed = Mathf.Lerp(speed, bottomSpeed, 0.5f); }

        if (q && canDash) { StartCoroutine(Glide()); }

        if (escape) {  }



        if (space && canDash) { StartCoroutine(Dash()); }


        if (rotateRight == 1 && rotateLeft == 1) { }
        else if (rotateForward == 1 && rotateBack == 1) { }
        else
        {
            rotationAngle = rotateBack * -180 + rotateForward * 0 + rotateLeft * -90 + rotateRight * 90;

            if ((rotateRight + rotateLeft + rotateForward + rotateBack) == 0) { rotationAngle = 0; }
            else { rotationAngle = rotationAngle / (rotateRight + rotateLeft + rotateForward + rotateBack); }
        }


        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotationAngle, 0), Time.deltaTime * 8);
        transform.position += movement * speed * speedScale * Time.deltaTime;

        movement = Vector3.zero;
        rotateRight = 0;
        rotateLeft = 0;
        rotateForward = 0;
        rotateBack = 0;
    }


    void GetPressedKeys()
    {

        w = Input.GetKey(KeyCode.W);
        a = Input.GetKey(KeyCode.A);
        s = Input.GetKey(KeyCode.S);
        d = Input.GetKey(KeyCode.D);
        shift = Input.GetKey(KeyCode.LeftShift);

        e = Input.GetKeyDown(KeyCode.E);
        q = Input.GetKeyDown(KeyCode.Q);
        space = Input.GetKeyDown(KeyCode.Space);
        escape = Input.GetKeyDown(KeyCode.Escape);
    }


    private IEnumerator Glide()
    {
        canDash = false;
        isGlading = true;
        rb.useGravity = false;
        rb.velocity = transform.forward * gladingPower;
        yield return new WaitForSeconds(glideTime);
        rb.useGravity = true;
        isGlading = false;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(glideCoolDown);
        canDash = true;
    }


    private IEnumerator Dash()
    {

        tr.emitting = true;
        canDash = false;
        isDashing = true;
        rb.useGravity = false;
        rb.velocity = transform.forward * dashPower;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.useGravity = true;
        isDashing = false;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

    void Jump()
    {
        if (canJump)
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
        canJump = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            canJump = true;

        }
    }

    public void HitEnemy()
    {
        if(isDashing)
        {
            comboScore++;
            audioSpeed += 0.2f; 
        }
        else
        {
            speedScale = 0.6f;
            StartCoroutine(speedUpAgain());
        }
    }

    private IEnumerator speedUpAgain()
    {
        yield return new WaitForSeconds(nerfTime);
        speedScale = 1;
    }   




}
