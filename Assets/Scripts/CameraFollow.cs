using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;


    private void Update()
    {
        transform.position = GameObject.Find("Player Unit").transform.position + offset;
    }
}
