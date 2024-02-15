using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyUnit : MonoBehaviour
{
    public float attackRange;
    public Vector2 attackCooldown;

    public GameObject[] spawnOnDeath;
    
    private Animator _animator;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    

    private void OnEnable()
    {
        _animator = GetComponentInChildren<Animator>();
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

    public void OnDeath()
    {
        StopAllCoroutines();
        
        foreach (var go in spawnOnDeath)
        {
            Instantiate(go, transform.position, transform.rotation, null);
        }
        
        Destroy(gameObject);
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
