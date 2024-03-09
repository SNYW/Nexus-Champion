using System.Collections.Generic;
using Spells.SpellEffects;
using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(menuName = "Game Data/Spell Effects/Damage Effect")]
    public class DamageSpellEffect : SpellEffect
    {
        public int damageAmount;
        public TargetingBehaviour targetingBehaviour;

        public override void Trigger(GameObject obj)
        {
            foreach (var unit in targetingBehaviour.GetTargets(obj.transform.position))
            {
               unit.OnHit(obj.transform, damageAmount);
            }
        }
    }
}