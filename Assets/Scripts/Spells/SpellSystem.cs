using Spells;
using SystemEvents;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{

    public GameObject projectileAnchor;
    private void OnEnable()
    {
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SpellCast, TryCastSpell);
    }

    private void TryCastSpell(object spell)
    {
        if (spell is not Spell) return;

        if (spell is ProjectileSpell ps)
            CastProjectileSpell(ps);
    }

    private void CastProjectileSpell(ProjectileSpell ps)
    {
        var dirToMouse = MouseManager.GetDirectionToMouse(transform.position);
        var projectile = ObjectPoolManager.GetPool(ps.projectileName).GetPooledObject();

        projectile.transform.position = projectileAnchor.transform.position;
        projectile.transform.forward = dirToMouse;
        transform.forward = dirToMouse;
        projectile.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SpellCast, TryCastSpell);
    }
}
