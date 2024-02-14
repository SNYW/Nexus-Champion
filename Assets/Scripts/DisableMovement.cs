using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovement : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindObjectOfType<NavMeshMovementSystem>().canMove = false;
    }

   
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindObjectOfType<NavMeshMovementSystem>().canMove = true;
    }

}
