
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] float Speed;

    void LateUpdate()
    {
        Quaternion OriginalRot = transform.rotation;
        transform.LookAt(Player.Instance.transform.position);
        Quaternion NewRot = transform.rotation;
        transform.rotation = OriginalRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, Speed * Time.deltaTime);

    }

}
