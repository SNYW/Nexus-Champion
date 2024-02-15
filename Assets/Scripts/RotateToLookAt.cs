using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToLookAt : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        RotateTowardsTarget();
    }
    
    void RotateTowardsTarget()
    {
        Vector3 directionToTarget = GameManager.playerUnit.transform.position - transform.position;
        directionToTarget.y = 0f; // We only want rotation on the Y axis
        if (directionToTarget == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
