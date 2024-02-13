using Spells;
using SystemEvents;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    public Spell[] spells;
    public GameObject projectileAnchor;

    private Animator _animator;
    private Vector3 dirToMouse;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellCast);
    }

    private void OnSpellCast(object obj)
    {
        _animator.Play("Attacking");
        dirToMouse = MouseManager.GetDirectionToMouse(transform.position);
        transform.parent.forward = dirToMouse;
    }

    public void CastSpellFromAnimation(int index)
    {
        TryCastSpell(spells[index]);
    }

    private void TryCastSpell(object spell)
    {
        if (spell is not Spell) return;

        if (spell is ProjectileSpell ps)
            CastProjectileSpell(ps);
    }

    private void CastProjectileSpell(ProjectileSpell ps)
    {
       
        var projectile = ObjectPoolManager.GetPool(ps.projectileName).GetPooledObject();

        projectile.transform.position = projectileAnchor.transform.position;
        projectile.transform.forward = dirToMouse;
        projectile.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellCast);
    }
}
