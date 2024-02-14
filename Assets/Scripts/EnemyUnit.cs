using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyUnit : MonoBehaviour
{
    public float attackRange;
    public Vector2 attackCooldown;
    
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
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
