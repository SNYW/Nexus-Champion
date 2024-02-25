using Spells;
using SystemEvents;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    public LayerMask mask;
    public Spell[] spells;
    public GameObject projectileAnchor;

    private Animator _animator;
    private Vector3 dirToMouse;
    private CastIndicator _indicator;

    private void OnEnable()
    {
        _animator = GetComponentInChildren<Animator>();
        _indicator = GetComponentInChildren<CastIndicator>();
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellCast);
    }

    private void OnSpellCast(object obj)
    {
        _animator.Play("Attacking");
        transform.forward = _indicator.transform.forward;
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
       
        var projectile = ObjectPoolManager.GetPool(ps.projectileName).GetPooledObject().GetComponent<Projectile>();

        projectile.transform.position = projectileAnchor.transform.position;
        projectile.transform.forward = _indicator.transform.forward;
        projectile.gameObject.SetActive(true);
        projectile.InitProjectile();
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellCast);
    }
}
