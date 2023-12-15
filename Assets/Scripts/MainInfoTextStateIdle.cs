using UnityEngine;

public class MainInfoTextStateIdle : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MainManager.Instance.isMainAnimationFinished = true;
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MainManager.Instance.isMainAnimationFinished = false;
    }
}
