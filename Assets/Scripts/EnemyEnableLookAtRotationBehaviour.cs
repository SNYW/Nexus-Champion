using UnityEngine;

public class EnemyEnableLookAtRotationBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.GetComponent<EnemyUnitBinder>().boundUnit.GetComponentInChildren<RotateToLookAt>(true).enabled = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyUnitBinder>().boundUnit.GetComponentInChildren<RotateToLookAt>(true ).enabled = false;
    }
}
