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

    // Define vars for dissolve
    public MeshRenderer meshRenderer;
    private Material[] materials;
    public float dissolveRate = 0.001f;
    public float refreshRate = 0.002f;

    private void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody>(); 
        // Stop movement
        _rb.velocity = Vector3.zero;
        // Stop rotation
        _rb.angularVelocity = Vector3.zero;
        ApplyRandomForceAndTorque();

        // Check for materials then define
        if (meshRenderer != null)
            materials = meshRenderer.materials;
        // Start dissolve effect
        StartCoroutine(DissolveProc());

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

    IEnumerator DissolveProc()
    {
        if (materials.Length > 0)
        {
            float counter = 0;

            while (materials[0].GetFloat("_dissolveAmount") < 1) //exposed _dissolveAmount property from material's shader (material must use the dissolve shader)
            {
                //Increment dissolveAmount material property
                counter += dissolveRate;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].SetFloat("_dissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate); //delay between steps
            }
        }
    }
}
