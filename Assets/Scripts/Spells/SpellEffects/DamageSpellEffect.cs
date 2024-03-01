using Spells.SpellEffects;
using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(menuName = "Game Data/Spell Effects/Damage Effect")]
    public class DamageSpellEffect : SpellEffect
    {
        public int damageAmount;
        public int maxTargets;
        public LayerMask hitmask;
        public float hitRadius;
        

        public override void Trigger(GameObject obj)
        {
            Collider[] colliders = new Collider[maxTargets];
            if (Physics.OverlapSphereNonAlloc(obj.transform.position, hitRadius, colliders, hitmask) == 0) return;
            foreach (var collider in colliders)
            {
                if (collider == null || !collider.TryGetComponent<Unit>(out var component)) return;
      
                component.OnHit(obj.transform, damageAmount);
            }
        }
    }
}