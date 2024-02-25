using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;


    private void Update()
    {
        if(GameManager.playerUnit != null)
            transform.position = GameManager.playerUnit.transform.position + offset;
    }
}
