using UnityEngine;

public class Trap : MonoBehaviour
{
    Area area;

    void Start()
    {
        area = transform.parent.parent.GetComponent<Area>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && area != null) area.PlayerFailed();
    }
}
