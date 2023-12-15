using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    private const float AfterButtonBackDelay = 0.5f;
    private const float AfterTransitionDelay = 1f;
    private const float SettingsShowDelay = 0.1f;
    
    private static readonly int FirstHash = Animator.StringToHash("First");
    private static readonly int ShowHash = Animator.StringToHash("Show");
    private static readonly int HideHash = Animator.StringToHash("Hide");
    private static readonly int SkipHash = Animator.StringToHash("Skip");
    
    [Header ("Main")]
    [SerializeField] private GameObject mainParent;
    
    [Space (10)]
    [FormerlySerializedAs("title")]
    [SerializeField] private GameObject mainTitle;
    [SerializeField] private GameObject infoText;
    
    [Space (10)]
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject quitButton;
    
    [Space (10)]
    [SerializeField] private GameObject startButtonBack;
    [SerializeField] private GameObject settingsButtonBack;
    [SerializeField] private GameObject quitButtonBack;
    
    [Header ("Settings")]
    [SerializeField] private GameObject settingsParent;
    
    [Space (10)]
    [SerializeField] private GameObject settingsTitle;
    [SerializeField] private GameObject settingsButtons;
    [SerializeField] private GameObject settingsBackButton;

    [Header ("Transition")]
    [SerializeField] private GameObject transition;
    

    private Animator _mainTitleAnim;
    private Animator _infoTextAnim;
    private Animator _startButtonAnim;
    private Animator _settingsButtonAnim;
    private Animator _quitButtonAnim;
    private Animator _settingsTitleAnim;
    private Animator _settingsBackButtonAnim;
    
    private List<Animator> _settingsButtonAnims = new();

    [HideInInspector] public bool isMainAnimationFinished;

    public static MainManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        isMainAnimationFinished = false;

        infoText.GetComponent<Text>().text = $"v{Application.version}";
        
        _mainTitleAnim = mainTitle.GetComponent<Animator>();
        _infoTextAnim = infoText.GetComponent<Animator>();
        _startButtonAnim = startButton.GetComponent<Animator>();
        _settingsButtonAnim = settingsButton.GetComponent<Animator>();
        _quitButtonAnim = quitButton.GetComponent<Animator>();
        _settingsTitleAnim = settingsTitle.GetComponent<Animator>();
        _settingsBackButtonAnim = settingsBackButton.GetComponent<Animator>();
        
        foreach (var anim in settingsButtons.GetComponentsInChildren<Animator>())
        {
            _settingsButtonAnims.Add(anim);
        }
        
        _mainTitleAnim.SetTrigger(FirstHash);
        _infoTextAnim.SetTrigger(FirstHash);
        _startButtonAnim.SetTrigger(FirstHash);
        _settingsButtonAnim.SetTrigger(FirstHash);
        _quitButtonAnim.SetTrigger(FirstHash);
    }

    private void Update()
    {
        if (!isMainAnimationFinished && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            isMainAnimationFinished = true;
        
            _mainTitleAnim.SetTrigger(SkipHash);
            _infoTextAnim.SetTrigger(SkipHash);
            _startButtonAnim.SetTrigger(SkipHash);
            _settingsButtonAnim.SetTrigger(SkipHash);
            _quitButtonAnim.SetTrigger(SkipHash);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // TODO
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // TODO
        }
    }

    private IEnumerator StartAnimation()
    {
        _settingsButtonAnim.SetTrigger(HideHash);
        _quitButtonAnim.SetTrigger(HideHash);
        
        startButtonBack.SetActive(true);
        startButtonBack.GetComponent<Animator>().SetTrigger(ShowHash);

        yield return new WaitForSeconds(AfterButtonBackDelay);
        
        transition.SetActive(true);
        transition.GetComponent<Animator>().SetTrigger(ShowHash);
        
        yield return new WaitForSeconds(AfterTransitionDelay);
    }
    
    private IEnumerator SettingsAnimation()
    {
        var settingsButtonBackAnim = settingsButtonBack.GetComponent<Animator>();
        
        _startButtonAnim.SetTrigger(HideHash);
        _quitButtonAnim.SetTrigger(HideHash);
        
        settingsButtonBack.SetActive(true);
        settingsButtonBackAnim.SetTrigger(ShowHash);
        _mainTitleAnim.SetTrigger(HideHash);
        _infoTextAnim.SetTrigger(HideHash);
        
        yield return new WaitForSeconds(AfterButtonBackDelay);
        
        settingsButtonBackAnim.SetTrigger(HideHash);
        settingsParent.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        
        _settingsTitleAnim.SetTrigger(ShowHash);
        
        yield return new WaitForSeconds(SettingsShowDelay);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(ShowHash);
            yield return new WaitForSeconds(SettingsShowDelay);
        }
        
        _settingsBackButtonAnim.SetTrigger(ShowHash);
        
        mainParent.SetActive(false);
        
    }

    private IEnumerator QuitAnimation()
    {
        _startButtonAnim.SetTrigger(HideHash);
        _settingsButtonAnim.SetTrigger(HideHash);
        
        quitButtonBack.SetActive(true);
        quitButtonBack.GetComponent<Animator>().SetTrigger(ShowHash);

        yield return new WaitForSeconds(AfterButtonBackDelay);
        
        transition.SetActive(true);
        transition.GetComponent<Animator>().SetTrigger(ShowHash);

        yield return new WaitForSeconds(AfterTransitionDelay);
        
        GameManager.QuitGame();
    }

    private IEnumerator SettingsBackAnimation()
    {
        _settingsTitleAnim.SetTrigger(HideHash);
        _settingsBackButtonAnim.SetTrigger(HideHash);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(HideHash);
        }
        
        yield return new WaitForSeconds(0.2f);
        
        mainParent.SetActive(true);
        settingsParent.SetActive(false);
        
        _mainTitleAnim.SetTrigger(ShowHash);
        _infoTextAnim.SetTrigger(ShowHash);
        _startButtonAnim.SetTrigger(ShowHash);
        _settingsButtonAnim.SetTrigger(ShowHash);
        _quitButtonAnim.SetTrigger(ShowHash);
    }
    
    public void OnStartButtonPressed()
    {
        StartCoroutine(StartAnimation());
    }
    
    public void OnSettingsButtonPressed()
    {
        StartCoroutine(SettingsAnimation());
    }
    
    public void OnQuitButtonPressed()
    {
        StartCoroutine(QuitAnimation());
    }

    public void OnSettingsBackButtonPressed()
    {
        StartCoroutine(SettingsBackAnimation());
    }
}
