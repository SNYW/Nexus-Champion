using System.Collections;
using ObjectPooling;
using Spells;
using Spells.SpellEffects;
using UnityEngine;

public class Projectile : PooledObject
{
  public float speed;
  public float lifetime;
  
  [SerializeReference]
  public SpellEffect[] onCastEffects;
  
  [SerializeReference]
  public SpellEffect[] onHitEffects;
  
  private void OnEnable()
  {
    StopAllCoroutines();
    TriggerAllEffects(onCastEffects);
  }
  
  
  public void InitProjectile()
  {
    StartCoroutine(Deactivate());
  }

  private void TriggerAllEffects(SpellEffect[] effects)
  {
    foreach (var effect in effects) 
    { 
      effect.Trigger(gameObject);
    }
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

  private void Die()
  {
    TriggerAllEffects(onHitEffects);
    StopAllCoroutines();
    ReQueue();
  }
}
