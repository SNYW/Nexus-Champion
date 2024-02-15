using System;
using System.Collections;
using ObjectPooling;
using UnityEngine;

public class Projectile : PooledObject
{
  public bool hitOnAwake;
  public LayerMask hitmask;
  public float speed;
  public float lifetime;
  public float hitRadius;
  public PooledObject[] spawnedOnDeath;
  

  private void OnEnable()
  {
    StopAllCoroutines();
    StartCoroutine(Deactivate());
    
    if(hitOnAwake)
      DealDamage();
  }

  private void Update()
  {
    transform.Translate(Vector3.forward * speed * Time.deltaTime);
  }

  private IEnumerator Deactivate()
  {
    yield return new WaitForSeconds(lifetime);
    Die();
  }

  private void OnTriggerEnter(Collider other)
  {
    Die();
  }

  private void DealDamage()
  {
    if (hitRadius == 0) return;
    
    var colliders = Physics.OverlapSphere(transform.position, hitRadius, hitmask);
    foreach (var collider in colliders)
    {
      if (collider.TryGetComponent<EnemyUnit>(out var component)) 
        component.OnDeath(transform);
    }
  }

  private void Die()
  {
    StopAllCoroutines();
    if(!hitOnAwake) DealDamage();
    SpawnAllChildren();
    ReQueue();
  }

  private void SpawnAllChildren()
  {
    foreach (var co in spawnedOnDeath)
    {
      var child = ObjectPoolManager.GetPool(co.objectPoolName).GetPooledObject();
      child.transform.position = transform.position;
      child.transform.rotation = transform.rotation;
      child.SetActive(true);
    }
  }
  
  void OnDrawGizmos()
  {
    
    var segments = 36;
    Vector3 center = transform.position;

    float angleIncrement = 360f / segments;
    float currentAngle = 0f;

    Vector3 lastPoint = center + new Vector3(hitRadius, 0f, 0f);
    for (int i = 0; i < segments; i++)
    {
      currentAngle += angleIncrement;
      float newX = center.x + Mathf.Cos(Mathf.Deg2Rad * currentAngle) * hitRadius;
      float newZ = center.z + Mathf.Sin(Mathf.Deg2Rad * currentAngle) * hitRadius;
      Vector3 newPoint = new Vector3(newX, center.y, newZ);

      Debug.DrawLine(lastPoint, newPoint, Color.red);

      lastPoint = newPoint;
    }

    // Connect the last point to the first point to complete the circle
    Debug.DrawLine(lastPoint, center + new Vector3(hitRadius, 0f, 0f), Color.red);
  }
}
