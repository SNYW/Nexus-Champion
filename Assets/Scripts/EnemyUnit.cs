using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using ObjectPooling;

public class EnemyUnit : MonoBehaviour
{
    public float attackRange;
    public Vector2 attackCooldown;

    //public GameObject[] spawnOnDeath; //delete
    public ObjectPool.ObjectPoolName[] spawnOnDeath;

    
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
            //Instantiate(go, transform.position, transform.rotation, null); //delete
            var obj = ObjectPoolManager.GetPool(go).GetPooledObject();
            obj.SetActive(true);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
           

        }

        Destroy(gameObject);
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
