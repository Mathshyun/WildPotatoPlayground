using UnityEngine;

public class TransitionBehaviour : MonoBehaviour
{
    public const float AfterTransitionShowDelay = 0.5f;
    
    private static readonly int ShowHash = Animator.StringToHash("Show");
    private static readonly int HideHash = Animator.StringToHash("Hide");
    
    private Animator _animator;
    private RectTransform _transitionRect;
    private RectTransform _baseRect;
    private RectTransform _leftRect;
    private RectTransform _rightRect;
    
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
        _animator = GetComponentInChildren<Animator>();
        _transitionRect = transform.GetChild(0).GetComponent<RectTransform>();
        _baseRect = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        _leftRect = transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        _rightRect = transform.GetChild(0).GetChild(2).GetComponent<RectTransform>();
        
        SetSize();
    }

    public void SetSize()
    {
        _transitionRect.sizeDelta = new Vector2(Screen.width + Screen.height, Screen.height);
        _baseRect.sizeDelta = new Vector2(Screen.width, 0f);
        _leftRect.sizeDelta = new Vector2(Screen.height / 2f, 0f);
        _rightRect.sizeDelta = new Vector2(Screen.height / 2f, 0f);
    }

    public void ShowAnimation()
    {
        _animator.SetTrigger(ShowHash);
    }
    
    public void HideAnimation()
    {
        _animator.SetTrigger(HideHash);
    }
}
