using System.Collections;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    private static readonly int ShowHash = Animator.StringToHash("Show");
    private static readonly int HideHash = Animator.StringToHash("Hide");
    
    [SerializeField] private Animator titleAnim;
    // [SerializeField] private Animator descriptionAnim;
    [SerializeField] private Animator contentsScrollViewAnim;
    [SerializeField] private Animator backButtonAnim;

    private bool _isAnimationPlaying;
    
    private void Start()
    {
        _isAnimationPlaying = true;
        StartCoroutine(EnterStartAnimation());
    }

    private IEnumerator EnterStartAnimation()
    {
        TransitionBehaviour.Instance.HideAnimation();
        yield return new WaitForSeconds(0.5f);
        titleAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        // descriptionAnim.SetTrigger(ShowHash);
        contentsScrollViewAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        backButtonAnim.SetTrigger(ShowHash);
        _isAnimationPlaying = false;
    }

    private IEnumerator ExitStartAnimation()
    {
        titleAnim.SetTrigger(HideHash);
        // descriptionAnim.SetTrigger(HideHash);
        contentsScrollViewAnim.SetTrigger(HideHash);
        backButtonAnim.SetTrigger(HideHash);
        yield return new WaitForSeconds(0.3f);
        
        TransitionBehaviour.Instance.ShowAnimation();

        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => TransitionBehaviour.Instance.IsTransitionFinished());
        
        GameManager.Instance.LoadMain();
    }

    private IEnumerator StartAnimation(string sceneName)
    {
        TransitionBehaviour.Instance.ShowAnimation();

        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => TransitionBehaviour.Instance.IsTransitionFinished());
        
        GameManager.Instance.LoadScene(sceneName);
    }
    
    public void OnBackButtonPressed()
    {
        if (_isAnimationPlaying) return;

        _isAnimationPlaying = true;
        
        StartCoroutine(ExitStartAnimation());
    }

    public void StartTestItem(string sceneName)
    {
        if (_isAnimationPlaying) return;

        _isAnimationPlaying = true;
        
        StartCoroutine(StartAnimation(sceneName));
    }
}
