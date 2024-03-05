using Spells;
using SystemEvents;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    public LayerMask mask;
    public GameObject projectileAnchor;

    private Animator _animator;
    private Vector3 dirToMouse;
    private CastIndicator _indicator;
    private Spell _chosenSpell;
    private Vector3 _cachedMousePos;

    private void OnEnable()
    {
        _animator = GetComponentInChildren<Animator>();
        _indicator = GetComponentInChildren<CastIndicator>();
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellCast);
    }

    private void OnSpellCast(object obj)
    {
        if (obj is not Spell s) return;
        
        _chosenSpell = s;
        MouseManager.GetMousePositionOnNavmesh(mask, transform.position, out var pos);
        _cachedMousePos = pos;

        _animator.Play("Attacking");
        transform.forward = _indicator.transform.forward;
    }

    public void CastSpellFromAnimation()
    {
        TryCastSpell(_chosenSpell);
        _chosenSpell = null;
    }

    private void TryCastSpell(object spell)
    {
        if (spell is not Spell) return;

        switch (spell)
        {
            case ProjectileSpell ps:
                CastSpell(ps);
                break;
            case AOESpell es:
                CastSpell(es);
                break;
        }
    }

    private void CastSpell(ProjectileSpell ps)
    {
       
        var projectile = ps.projectile.GetPooledObject().GetComponent<Projectile>();

        projectile.transform.position = projectileAnchor.transform.position;
        projectile.transform.forward = _indicator.transform.forward;
        projectile.gameObject.SetActive(true);
        projectile.InitProjectile();
    }
    
    private void CastSpell(AOESpell ps)
    {
       
        var projectile = ps.projectile.GetPooledObject().GetComponent<Projectile>();

            projectile.transform.position = _cachedMousePos;
            projectile.gameObject.SetActive(true);
            projectile.InitProjectile();
    }


    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellCast);
    }
}
