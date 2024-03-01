using System.Collections;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    private static readonly int ShowHash = Animator.StringToHash("Show");
    private static readonly int HideHash = Animator.StringToHash("Hide");
    
    [SerializeField] private Animator titleAnim;
    [SerializeField] private Animator descriptionAnim;
    [SerializeField] private Animator backButtonAnim;
    
    private void Start()
    {
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        TransitionBehaviour.Instance.HideAnimation();
        yield return new WaitForSeconds(0.5f);
        titleAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        descriptionAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        backButtonAnim.SetTrigger(ShowHash);
    }

    private IEnumerator BackAnimation()
    {
        titleAnim.SetTrigger(HideHash);
        descriptionAnim.SetTrigger(HideHash);
        backButtonAnim.SetTrigger(HideHash);
        yield return new WaitForSeconds(0.3f);
        
        TransitionBehaviour.Instance.ShowAnimation();

        yield return new WaitUntil(() => TransitionBehaviour.Instance.IsTransitionFinished());
        
        GameManager.Instance.LoadMain();
    }
    
    public void OnBackButtonPressed()
    {
        StartCoroutine(BackAnimation());
    }
}
