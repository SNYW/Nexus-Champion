using ObjectPooling;
using UnityEngine;

public class Unit : PooledObject
{
    public int maxHealth;
    public int currentHealth;
    public Collider hitCollider;

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public virtual void OnHit(Transform origin, int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
            OnDeath(origin);
    }

    protected virtual void OnDeath(Transform origin)
    {
        ReQueue();
    }
}
