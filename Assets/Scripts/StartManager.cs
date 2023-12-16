using System.Collections;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    private static readonly int ShowHash = Animator.StringToHash("Show");
    private static readonly int HideHash = Animator.StringToHash("Hide");
    
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject transition;

    private Animator _titleAnim;
    private Animator _descAnim;
    private Animator _backAnim;
    private Animator _transAnim;
    
    private void Start()
    {
        _titleAnim = title.GetComponent<Animator>();
        _descAnim = description.GetComponent<Animator>();
        _backAnim = backButton.GetComponent<Animator>();
        _transAnim = transition.GetComponent<Animator>();
        
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        _transAnim.SetTrigger(HideHash);
        yield return new WaitForSeconds(0.5f);
        _titleAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        _descAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        _backAnim.SetTrigger(ShowHash);
    }

    private IEnumerator BackAnimation()
    {
        _titleAnim.SetTrigger(HideHash);
        _descAnim.SetTrigger(HideHash);
        _backAnim.SetTrigger(HideHash);
        yield return new WaitForSeconds(0.3f);
        
        transition.SetActive(true);
        transition.GetComponent<Animator>().SetTrigger(ShowHash);

        yield return new WaitForSeconds(TransitionBehaviour.AfterTransitionShowDelay);

        GameManager.Instance.LoadMain();
    }
    
    public void OnBackButtonPressed()
    {
        StartCoroutine(BackAnimation());
    }
}
