using System.Collections.Generic;
using UnityEngine;

namespace Spells
{ 
    public abstract class TargetingBehaviour : ScriptableObject
    {
        public abstract List<Unit> GetTargets(Vector3 position);
    }
}
    
