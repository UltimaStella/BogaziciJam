using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public Vector3 initialOffset;

    private void Start()
    {
        UpdateOffset();
    }

    void LateUpdate()
    {
        float PlayerX = Player.Instance.transform.position.x;
        Vector3 TargetPos = Player.Instance.transform.position + initialOffset;
        transform.position = TargetPos;
    }

    public void UpdateOffset()
    {
        initialOffset = transform.position - Player.Instance.transform.position;
    }
}
