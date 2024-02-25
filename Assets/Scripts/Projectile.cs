using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class Projectile : PooledObject
{
  public bool hitOnAwake;
  public LayerMask hitmask;
  public float speed;
  public float lifetime;
  public int damage;
  public float hitRadius;
  public PooledObject[] spawnedOnDeath;
  
  private void OnEnable()
  {
    StopAllCoroutines();
  }
  public void InitProjectile()
  {
    StartCoroutine(Deactivate());
    
    if(hitOnAwake)
      DealDamage();
  }
  private void Update()
  {
    transform.Translate(Vector3.forward * (speed * Time.deltaTime));
  }

  private IEnumerator Deactivate()
  {
    yield return new WaitForSeconds(lifetime);
    Die();
  }

  private void OnCollisionEnter(Collision collision)
  {
    Die();
  }

  protected virtual void DealDamage()
  {
    if (hitRadius == 0) return;

    Collider[] colliders = new Collider[10];
    if (Physics.OverlapSphereNonAlloc(transform.position, hitRadius, colliders, hitmask) == 0) return;
    foreach (var collider in colliders)
    {
      if (collider == null || !collider.TryGetComponent<Unit>(out var component)) return;
      
      component.OnHit(transform, damage);
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
      child.GetComponent<Projectile>().InitProjectile();
    }
  }
  
  void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, hitRadius);
  }
}
