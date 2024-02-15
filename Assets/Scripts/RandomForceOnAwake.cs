using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using ObjectPooling;

public class RandomForceOnAwake : PooledObject
{
    public float minForce = 10f;
    public float maxForce = 20f;
    public float minTorque = 10f;
    public float maxTorque = 20f;
    public Transform forceOrigin;
    public float forceMultiplier = 1f;
    public float upForceMultiplier = 3f;

    private Rigidbody _rb;

    // Define vars for dissolve
    public MeshRenderer meshRenderer;
    public float dissolveDelay = 1;


    private void OnEnable()
    {

        _rb = GetComponentInChildren<Rigidbody>(); 
        // Stop movement
        _rb.velocity = Vector3.zero;
        // Stop rotation
        _rb.angularVelocity = Vector3.zero;

    }

    private void Awake()
    {
        meshRenderer.material = Instantiate(meshRenderer.materials[0]);
    }

    public void ApplyRandomForceAndTorque()
    {
        Rigidbody rb = GetComponentInChildren<Rigidbody>();
        if (rb == null) return;
        
        Vector3 forceDirection = (transform.position - forceOrigin.position).normalized * forceMultiplier;
        forceDirection.y = 0;
        
        Vector3 randomForce = Vector3.up * upForceMultiplier + forceDirection * Random.Range(minForce, maxForce);
        rb.AddForce(randomForce, ForceMode.Impulse);

        Vector3 randomTorque = Random.insideUnitSphere * Random.Range(minTorque, maxTorque);
        rb.AddTorque(randomTorque, ForceMode.Impulse);

        GetComponent<DissolveController>().DissolveOut(dissolveDelay);

    }


    private void OnDisable()
    {
        StopAllCoroutines();
        ReQueue();
    }


}
