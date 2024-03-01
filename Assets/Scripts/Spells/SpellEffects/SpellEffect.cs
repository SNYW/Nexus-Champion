using System;
using UnityEngine;

namespace Spells.SpellEffects
{
    public abstract class SpellEffect : ScriptableObject
    {
        public abstract void Trigger(GameObject obj);
    }
}