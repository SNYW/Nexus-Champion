using System;
using System.Collections;
using ObjectPooling;
using UnityEngine;

public class Projectile : PooledObject
{
  public float speed;
  public float lifetime;
  public PooledObject[] spawnedOnDeath;

  private void OnEnable()
  {
    StopAllCoroutines();
    StartCoroutine(Deactivate());
  }

  private void Update()
  {
    transform.Translate(Vector3.forward * speed * Time.deltaTime);
  }

  private IEnumerator Deactivate()
  {
    yield return new WaitForSeconds(lifetime);
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
}
