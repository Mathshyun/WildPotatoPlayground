using UnityEngine;

public class TransitionStateIdle : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        animator.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        animator.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    }
}
