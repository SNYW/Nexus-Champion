using System.Collections;
using ObjectPooling;
using UnityEngine;

public class Projectile : PooledObject
{
  public float speed;
  public float lifetime;

  private void Start()
  {
    StopAllCoroutines();
    Invoke(nameof(Deactivate), lifetime);
  }

  private void Update()
  {
    transform.Translate(Vector3.forward * speed * Time.deltaTime);
  }

  private IEnumerator Deactivate()
  {
    yield return new WaitForSeconds(lifetime);
    ReQueue();
  }
}
