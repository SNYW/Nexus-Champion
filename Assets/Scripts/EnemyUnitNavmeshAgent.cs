using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyUnitNavmeshAgent : MonoBehaviour
{
   public float maxFollowRange;
   public float minFollowRange;
   public float maxPathFindingUpdateSpeed;
   public float minPathFindingUpdateSpeed;
   private NavMeshAgent _agent;
   private GameObject _playerUnit;

   private void OnEnable()
   {
      if(_agent == null)
         _agent = GetComponent<NavMeshAgent>();

      _playerUnit = GameManager.playerUnit;

      StartCoroutine(GetPath());
   }

   private IEnumerator GetPath()
   {
      while (gameObject.activeSelf)
      {
         MoveToEdgeOfCircleAroundPlayer();
         yield return new WaitForSeconds(Random.Range(maxPathFindingUpdateSpeed, minPathFindingUpdateSpeed));
      }
   }

   void MoveToPositionAwayFromPlayer()
   {
      Vector3 directionToPlayer = transform.position - _playerUnit.transform.position;
      directionToPlayer = directionToPlayer.normalized * maxFollowRange;
      Vector3 targetPosition = _playerUnit.transform.position + directionToPlayer;

      NavMeshHit hit;
      if (NavMesh.SamplePosition(targetPosition, out hit, minFollowRange, NavMesh.AllAreas))
      {
         _agent.SetDestination(hit.position);
      }
   }
   
   void MoveToEdgeOfCircleAroundPlayer()
   {
      float randomAngle = Random.Range(0f, 360f);
      Vector3 randomDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;
      float randomDistance = Random.Range(minFollowRange, maxFollowRange);
      Vector3 targetPosition = GameManager.playerUnit.transform.position + randomDirection * randomDistance;

      if (NavMesh.SamplePosition(targetPosition, out var hit, maxFollowRange, NavMesh.AllAreas))
      {
         _agent.SetDestination(hit.position);
      }
   }

   private void OnDisable()
   {
      StopAllCoroutines();
   }
}
