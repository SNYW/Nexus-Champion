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


    private Rigidbody _rb;

    // Define vars for dissolve
    public MeshRenderer meshRenderer;
    public float dissolveRate = 0.001f;
    public float refreshRate = 0.002f;


    private void OnEnable()
    {

        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            meshRenderer.materials[i].SetFloat("_dissolveAmount", 0);
        }

        _rb = GetComponentInChildren<Rigidbody>(); 
        // Stop movement
        _rb.velocity = Vector3.zero;
        // Stop rotation
        _rb.angularVelocity = Vector3.zero;
        

        // Start dissolve effect
        StartCoroutine(DissolveProc());

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
        
        Vector3 randomForce = forceDirection * Random.Range(minForce, maxForce);
        rb.AddForce(randomForce, ForceMode.Impulse);

        Vector3 randomTorque = Random.insideUnitSphere * Random.Range(minTorque, maxTorque);
        rb.AddTorque(randomTorque, ForceMode.Impulse);
    }

    IEnumerator DissolveProc()
    {
        if (meshRenderer.materials?.Length > 0)
        {
            float counter = 0;

            while (meshRenderer.materials[0].GetFloat("_dissolveAmount") < 1) //exposed _dissolveAmount property from material's shader (material must use the dissolve shader)
            {
                //Increment dissolveAmount material property
                counter += dissolveRate;
                for (int i = 0; i < meshRenderer.materials.Length; i++)
                {
                    meshRenderer.materials[i].SetFloat("_dissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate); //delay between steps
            }

            gameObject.SetActive(false);

        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
        ReQueue();
    }

}
