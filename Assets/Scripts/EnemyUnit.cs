using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using ObjectPooling;
using SystemEvents;

public class EnemyUnit : Unit
{
    public bool isActive;
    public float attackRange;
    public Transform castAnchor;
    public ObjectPool projectilePool;
    public Vector2 attackCooldown;

    public ObjectPool[] spawnOnDeath;
    
    private Animator _animator;
    private DissolveController _dissolveController;
    private EnemyUnitNavmeshAgent _agent;
    private UnitHealthbar _healthbar;
    
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    private static readonly int IsActive = Animator.StringToHash("isActive");

    public class EnemyDamageEvent 
    {
        public EnemyUnit unit;
        public int damageAmount;

        public EnemyDamageEvent(EnemyUnit unit, int damageAmount)
        {
            this.unit = unit;
            this.damageAmount = damageAmount;
        }
    }

    protected override void OnEnable()
    {
        isActive = false;
        _healthbar = GetComponentInChildren<UnitHealthbar>();
        _agent = GetComponent<EnemyUnitNavmeshAgent>();
        _agent.canMove = false;
        _animator = GetComponentInChildren<Animator>();
        hitCollider.enabled = false;
        
        _dissolveController = GetComponentInChildren<DissolveController>();
        _dissolveController.DissolveIn(0, Enable);
        base.OnEnable();
        _healthbar.Reset();
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.GameEnd, OnGameEnd);
    }

    private void OnGameEnd(object obj)
    { 
        OnDeath(transform);
    }

    private void Enable()
    {
        isActive = true;
        _agent.Enable();
        hitCollider.enabled = true;
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(attackCooldown.x, attackCooldown.y));
            if ((transform.position - GameManager.playerUnit.transform.position).magnitude >= attackRange) continue;
            
            transform.forward = GameManager.playerUnit.transform.position - transform.position;
            _animator.SetTrigger(AttackTrigger);
        }
    }

    public void FireProjectile()
    {
        var projectile = projectilePool.GetPooledObject().GetComponent<Projectile>();
        projectile.transform.position = castAnchor.transform.position;
        projectile.transform.forward = transform.forward;
        projectile.gameObject.SetActive(true);
        projectile.InitProjectile();
    }

    public override void OnHit(Transform origin, int damageAmount)
    {
        if (!isActive) return;
        
        base.OnHit(origin, damageAmount);
        var damageEvent = new EnemyDamageEvent(this, damageAmount);
        SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.EnemyDamaged, damageEvent);
        _healthbar.UpdateHealthIndicator(currentHealth, maxHealth);
    }

    private void Update()
    {
        _animator.SetBool(IsActive, isActive);
    }

    protected override void OnDeath(Transform origin)
    {
        foreach (var go in spawnOnDeath)
        {
            var obj = go.GetPooledObject();
            if(obj == null) return;
            
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);

           if (obj.TryGetComponent<RandomForceOnAwake>(out var component))
            {
                
                component.forceOrigin = origin;
                component.ApplyRandomForceAndTorque();
            }
        }
        
        base.OnDeath(origin);
    }
    
    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.GameEnd, OnGameEnd);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}
