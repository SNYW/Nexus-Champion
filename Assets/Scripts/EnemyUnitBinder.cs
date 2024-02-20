using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitBinder : MonoBehaviour
{
   public EnemyUnit boundUnit;

   public void DisableUnitMovement()
   {
      boundUnit.GetComponent<EnemyUnitNavmeshAgent>().canMove = false;
   }
   
   public void EnableUnitMovement()
   {
      boundUnit.GetComponent<EnemyUnitNavmeshAgent>().canMove = true;
   }

   public void FireProjectile()
   {
      boundUnit.FireProjectile();
   }
}
