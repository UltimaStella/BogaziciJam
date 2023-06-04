using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public Vector3 Offset;

    private void Start()
    {
        UpdateOffset();
    }

    void LateUpdate()
    {
        transform.position = Player.Instance.transform.position + Offset;
    }

    public void UpdateOffset()
    {
        Offset = transform.position - Player.Instance.transform.position;
    }
}
