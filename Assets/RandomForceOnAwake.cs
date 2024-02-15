using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomForceOnAwake : MonoBehaviour
{
    public float minForce = 10f;
    public float maxForce = 20f;
    public float minTorque = 10f;
    public float maxTorque = 20f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody>(); 
        // Stop movement
        _rb.velocity = Vector3.zero;
        // Stop rotation
        _rb.angularVelocity = Vector3.zero;
        ApplyRandomForceAndTorque();
    }

    void ApplyRandomForceAndTorque()
    {
        Rigidbody rb = GetComponentInChildren<Rigidbody>();
        if (rb == null) return;
        
        Vector3 randomForce = Random.insideUnitSphere * Random.Range(minForce, maxForce);
        rb.AddForce(randomForce, ForceMode.Impulse);

        Vector3 randomTorque = Random.insideUnitSphere * Random.Range(minTorque, maxTorque);
        rb.AddTorque(randomTorque, ForceMode.Impulse);
    }
}
