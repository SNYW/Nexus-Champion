using ObjectPooling;
using UnityEngine;

namespace Spells
{ 
    public abstract class Spell : ScriptableObject
    {
        public Sprite spellIcon;
        public float coolDown;
        public GameObject indicatorPrefab;
        public ObjectPool.ObjectPoolName projectileName;

        public abstract void TryCast();
    }
}
