using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using ObjectPooling;
using SystemEvents;

public class EnemyUnit : PooledObject
{
    public bool isActive;
    public int maxHealth;
    public int currentHealth;
    public float attackRange;
    public Transform castAnchor;
    public ObjectPool.ObjectPoolName projectilePoolName;
    public Vector2 attackCooldown;
    public Collider hitCollider;
    public ObjectPool.ObjectPoolName[] spawnOnDeath;
    
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

    private void OnEnable()
    {
        isActive = false;
        _healthbar = GetComponentInChildren<UnitHealthbar>();
        _agent = GetComponent<EnemyUnitNavmeshAgent>();
        _agent.canMove = false;
        _animator = GetComponentInChildren<Animator>();
        hitCollider.enabled = false;
        
        _dissolveController = GetComponentInChildren<DissolveController>();
        _dissolveController.DissolveIn(0, Enable);
        
        currentHealth = maxHealth;
        _healthbar.Reset();
    }

    private void Enable()
    {
        isActive = true;
        _agent.Enable();
        //hitCollider.enabled = true;
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
        var projectile = ObjectPoolManager.GetPool(projectilePoolName).GetPooledObject();
        projectile.transform.position = castAnchor.transform.position;
        projectile.transform.forward = transform.forward;
        projectile.gameObject.SetActive(true);
    }

    public void OnHit(Transform origin, int damageAmount)
    {
        if (!isActive) return;
        
        var damageEvent = new EnemyDamageEvent(this, damageAmount);
        currentHealth -= damageAmount;
        SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.EnemyDamaged, damageEvent);
        _healthbar.UpdateHealthIndicator(currentHealth, maxHealth);
        
        if(currentHealth <= 0)
            OnDeath(origin);
    }

    private void Update()
    {
        _animator.SetBool(IsActive, isActive);
    }

    public void OnDeath(Transform origin)
    {
        StopAllCoroutines();
        
        foreach (var go in spawnOnDeath)
        {
            var obj = ObjectPoolManager.GetPool(go).GetPooledObject();
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

        ReQueue();
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    
    void OnDrawGizmos()
    {
    
        var segments = 36;
        Vector3 center = transform.position;

        float angleIncrement = 360f / segments;
        float currentAngle = 0f;

        Vector3 lastPoint = center + new Vector3(attackRange, 0f, 0f);
       
        for (int i = 0; i < segments; i++)
        {
            currentAngle += angleIncrement;
            float newX = center.x + Mathf.Cos(Mathf.Deg2Rad * currentAngle) * attackRange;
            float newZ = center.z + Mathf.Sin(Mathf.Deg2Rad * currentAngle) * attackRange;
            Vector3 newPoint = new Vector3(newX, center.y, newZ);

            Debug.DrawLine(lastPoint, newPoint, Color.red);

            lastPoint = newPoint;
        }
        
        // Connect the last point to the first point to complete the circle
        Debug.DrawLine(lastPoint, center + new Vector3(attackRange, 0f, 0f), Color.red);
    }
}
