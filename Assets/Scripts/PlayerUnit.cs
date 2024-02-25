using SystemEvents;
using UnityEngine;

public class PlayerUnit : Unit
{
    public bool endGameOnDeath;

    public override void OnHit(Transform origin, int damageAmount)
    {
        base.OnHit(origin, damageAmount);
        SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.PlayerDamaged, this);
    }

    protected override void OnDeath(Transform origin)
    {
        SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.PlayerDamaged, this);
        
        if(endGameOnDeath)
            SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.GameEnd, null);
        
        base.OnDeath(origin);
    }
}
