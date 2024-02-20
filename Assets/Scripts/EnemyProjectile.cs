using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void DealDamage()
    {
        if (hitRadius == 0) return;
    
        var colliders = Physics.OverlapSphere(transform.position, hitRadius, hitmask);
        foreach (var collider in colliders)
        {
            Debug.Log("player hit");
        }
    }
}