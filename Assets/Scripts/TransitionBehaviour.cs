using UnityEngine;

public class TransitionBehaviour : MonoBehaviour
{
    public const float AfterTransitionShowDelay = 0.5f;
    
    private static readonly int ShowHash = Animator.StringToHash("Show");
    private static readonly int HideHash = Animator.StringToHash("Hide");
    
    private Animator animator;
    private RectTransform transitionRect;
    private RectTransform baseRect;
    private RectTransform leftRect;
    private RectTransform rightRect;
    
    public static TransitionBehaviour Instance { get; private set; }

    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        transitionRect = transform.GetChild(0).GetComponent<RectTransform>();
        baseRect = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        leftRect = transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        rightRect = transform.GetChild(0).GetChild(2).GetComponent<RectTransform>();
        
        SetSize();
    }

    public void SetSize()
    {
        transitionRect.sizeDelta = new Vector2(Screen.width + Screen.height, Screen.height);
        baseRect.sizeDelta = new Vector2(Screen.width, 0f);
        leftRect.sizeDelta = new Vector2(Screen.height / 2f, 0f);
        rightRect.sizeDelta = new Vector2(Screen.height / 2f, 0f);
    }

    public void ShowAnimation()
    {
        animator.SetTrigger(ShowHash);
    }
    
    public void HideAnimation()
    {
        animator.SetTrigger(HideHash);
    }
}
