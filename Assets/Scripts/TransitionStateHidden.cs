using UnityEngine;

public class TransitionStateHidden : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0.5f);
        animator.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0.5f);
        animator.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
    }
}
