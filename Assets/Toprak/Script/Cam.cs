using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private void Update()
    {
        float PlayerX = Player.Instance.transform.position.x;
        Vector3 TargetPos = new Vector3(PlayerX, transform.position.y, transform.position.z);
        transform.position = TargetPos;
    }
}
