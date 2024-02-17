using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using ObjectPooling;
using SystemEvents;

public class EnemyUnit : MonoBehaviour
{
    public float attackRange;
    public Vector2 attackCooldown;
    public Collider hitCollider;
    public ObjectPool.ObjectPoolName[] spawnOnDeath;

    
    private Animator _animator;
    private DissolveController _dissolveController;
    private EnemyUnitNavmeshAgent _agent;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    public bool isActive;

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
        _agent = GetComponent<EnemyUnitNavmeshAgent>();
        _agent.canMove = false;
        _animator = GetComponentInChildren<Animator>();
        hitCollider.enabled = false;
        _dissolveController = GetComponentInChildren<DissolveController>();
        
        _dissolveController.DissolveIn(0, Enable);
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
            _animator.SetTrigger(AttackTrigger);
        }
    }

    public void OnHit(Transform origin, int damageAmount)
    {
        var damageEvent = new EnemyDamageEvent(this, damageAmount);
        SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.EnemyDamaged, damageEvent);
        OnDeath(origin);
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

        Destroy(gameObject);
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
