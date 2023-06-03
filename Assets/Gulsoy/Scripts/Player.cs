using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float speed = 10f;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
    }

    public void Retry(Vector3 SpawnLoc)
    {
        transform.SetPositionAndRotation(SpawnLoc, Quaternion.identity);
        AreaManager.Instance.CurrentArea.ResetArea();
    }

    // kapilari denemek icin hareket lazimdir abe
    private void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        transform.position += speed * Time.deltaTime * new Vector3(xMove, 0, zMove);
    }
}
