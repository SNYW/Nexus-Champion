using System.Collections;
using ObjectPooling;
using Spells;
using Spells.SpellEffects;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : PooledObject
{
  public float speed;
  public float lifetime;
  public float onCastDelay;

  [Range(1,60)]
  public float effectUpdateRate;
  
  public VisualEffect[] worldPosEffects;
  
  [SerializeReference]
  public SpellEffect[] onCastEffects;
  
  [SerializeReference]
  public SpellEffect[] onHitEffects;
  
  private void OnEnable()
  {
    StopAllCoroutines();
    StartCoroutine(TriggerAllEffects(onCastEffects, onCastDelay));
    StartCoroutine(UpdateVisualEffects(effectUpdateRate));
    foreach (var effect in worldPosEffects)
    {
      if(effect.HasVector3("startPos")) 
        effect.SetVector3("startPos", transform.position);
    }
  }

  private IEnumerator UpdateVisualEffects(float updateRate)
  {
    while (gameObject.activeSelf)
    {
      foreach (var effect in worldPosEffects)
      {
        if(effect.HasVector3("worldPos")) 
          effect.SetVector3("worldPos", transform.position);
      }

      yield return new WaitForSeconds(1/updateRate);
    }
  }
  
  public void InitProjectile()
  {
    StartCoroutine(Deactivate());
  }

  private IEnumerator TriggerAllEffects(SpellEffect[] effects, float delay)
  {
    yield return new WaitForSeconds(delay);
    foreach (var effect in effects) 
    { 
      effect.Trigger(gameObject);
    }
  }
  
  private void TriggerAllEffectsImmediate(SpellEffect[] effects)
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
    TriggerAllEffectsImmediate(onHitEffects);
    StopAllCoroutines();
    ReQueue();
  }
}
