using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
    }

    // kapilari denemek icin hareket lazimdir abe
    private void Update()
    {
        var xMove = Input.GetAxisRaw("Horizontal");
        var zMove = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(xMove, 0, zMove) * speed * Time.deltaTime;
    }

    public void Retry(Vector3 SpawnLoc)
    {
        transform.SetPositionAndRotation(SpawnLoc, Quaternion.identity);
    }
}