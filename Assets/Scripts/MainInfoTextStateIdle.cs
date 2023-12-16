using UnityEngine;

public class MainInfoTextStateIdle : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.isMainAnimationFinished = true;
    }
}
