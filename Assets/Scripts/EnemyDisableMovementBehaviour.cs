using UnityEngine;

public class EnemyDisableMovementBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      animator.GetComponent<EnemyUnitBinder>().DisableUnitMovement();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyUnitBinder>().EnableUnitMovement();
    }
}
