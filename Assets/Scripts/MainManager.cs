using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    private const float AfterButtonBackDelay = 0.5f;
    private const float AfterHideDelay = 0.2f;
    private const float SettingsShowDelay = 0.1f;
    
    private static readonly int FirstHash = Animator.StringToHash("First");
    private static readonly int ShowHash = Animator.StringToHash("Show");
    private static readonly int HideHash = Animator.StringToHash("Hide");
    private static readonly int SkipHash = Animator.StringToHash("Skip");
    
    [Header ("Main")]
    [FormerlySerializedAs("title")]
    [SerializeField] private GameObject mainTitle;
    [SerializeField] private GameObject mainInfoText;
    
    [Space (10)]
    [SerializeField] private GameObject mainStartButton;
    [SerializeField] private GameObject mainSettingsButton;
    [SerializeField] private GameObject mainQuitButton;
    
    [Space (10)]
    [SerializeField] private GameObject mainStartButtonBack;
    [SerializeField] private GameObject mainSettingsButtonBack;
    [SerializeField] private GameObject mainQuitButtonBack;

    [Header("Settings")]
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private GameObject settingsTitle;
    [SerializeField] private GameObject settingsButtons;
    [SerializeField] private GameObject settingsBackButton;

    [Header("Video Settings")]
    [SerializeField] private GameObject videoSettingsTitle;
    [SerializeField] private GameObject videoSettingsButtons;
    [SerializeField] private GameObject videoSettingsApplyButton;
    [SerializeField] private GameObject videoSettingsBackButton;

    private Animator _mainTitleAnim;
    private Animator _infoTextAnim;
    private Animator _startButtonAnim;
    private Animator _settingsButtonAnim;
    private Animator _quitButtonAnim;
    private Animator _settingsTitleAnim;
    private Animator _settingsBackButtonAnim;
    private Animator _videoSettingsTitleAnim;
    private Animator _videoSettingsApplyButtonAnim;
    private Animator _videoSettingsBackButtonAnim;
    
    private IEnumerator _firstAnimation;
    
    private readonly List<Animator> _settingsButtonAnims = new();
    private readonly List<Animator> _videoSettingsButtonAnims = new();

    private static bool IsMainAnimationFinished
    {
        get => GameManager.Instance.isMainAnimationFinished;
        set => GameManager.Instance.isMainAnimationFinished = value;
    }
    
    private void Start()
    {
        mainInfoText.GetComponent<Text>().text = $"v{Application.version}";
        
        _mainTitleAnim = mainTitle.GetComponent<Animator>();
        _infoTextAnim = mainInfoText.GetComponent<Animator>();
        _startButtonAnim = mainStartButton.GetComponent<Animator>();
        _settingsButtonAnim = mainSettingsButton.GetComponent<Animator>();
        _quitButtonAnim = mainQuitButton.GetComponent<Animator>();
        
        _settingsTitleAnim = settingsTitle.GetComponent<Animator>();
        _settingsBackButtonAnim = settingsBackButton.GetComponent<Animator>();
        
        _videoSettingsTitleAnim = videoSettingsTitle.GetComponent<Animator>();
        _videoSettingsApplyButtonAnim = videoSettingsApplyButton.GetComponent<Animator>();
        _videoSettingsBackButtonAnim = videoSettingsBackButton.GetComponent<Animator>();
        
        
        foreach (var anim in settingsButtons.GetComponentsInChildren<Animator>())
        {
            _settingsButtonAnims.Add(anim);
        }
        
        foreach (var anim in videoSettingsButtons.GetComponentsInChildren<Animator>())
        {
            _videoSettingsButtonAnims.Add(anim);
        }

        if (!IsMainAnimationFinished)
        {
            _firstAnimation = FirstAnimation();
            StartCoroutine(_firstAnimation);
        }
        else
        {
            StartCoroutine(BackToMainSceneAnimation());
        }
        
        settingsManager.SetAllSettingsText();
        settingsManager.SetAllVideoSettingsText();
    }

    private void Update()
    {
        if (!IsMainAnimationFinished && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            IsMainAnimationFinished = true;
            
            StopCoroutine(_firstAnimation);
        
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
    
    private IEnumerator FirstAnimation()
    {
        _mainTitleAnim.SetTrigger(FirstHash);
        
        yield return new WaitForSeconds(1.5f);
        
        _startButtonAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        _settingsButtonAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        _quitButtonAnim.SetTrigger(ShowHash);
        _infoTextAnim.SetTrigger(ShowHash);
        
        yield return new WaitForSeconds(0.5f);
        
        IsMainAnimationFinished = true;
    }

    private IEnumerator StartAnimation()
    {
        _startButtonAnim.SetTrigger(HideHash);
        _settingsButtonAnim.SetTrigger(HideHash);
        _quitButtonAnim.SetTrigger(HideHash);
        
        mainStartButtonBack.GetComponent<Animator>().SetTrigger(ShowHash);

        yield return new WaitForSeconds(AfterButtonBackDelay);

        TransitionBehaviour.Instance.ShowAnimation();
        
        yield return new WaitForSeconds(TransitionBehaviour.AfterTransitionShowDelay);

        GameManager.Instance.LoadStart();
    }
    
    private IEnumerator SettingsAnimation()
    {
        var settingsButtonBackAnim = mainSettingsButtonBack.GetComponent<Animator>();
        
        _startButtonAnim.SetTrigger(HideHash);
        _settingsButtonAnim.SetTrigger(HideHash);
        _quitButtonAnim.SetTrigger(HideHash);
        
        settingsButtonBackAnim.SetTrigger(ShowHash);
        _mainTitleAnim.SetTrigger(HideHash);
        _infoTextAnim.SetTrigger(HideHash);
        
        yield return new WaitForSeconds(AfterButtonBackDelay);
        
        settingsButtonBackAnim.SetTrigger(HideHash);

        yield return new WaitForSeconds(AfterHideDelay);
        
        _settingsTitleAnim.SetTrigger(ShowHash);
        
        yield return new WaitForSeconds(SettingsShowDelay);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(ShowHash);
            yield return new WaitForSeconds(SettingsShowDelay);
        }

        _settingsBackButtonAnim.SetTrigger(ShowHash);
    }

    private IEnumerator QuitAnimation()
    {
        _startButtonAnim.SetTrigger(HideHash);
        _settingsButtonAnim.SetTrigger(HideHash);
        _quitButtonAnim.SetTrigger(HideHash);
        
        mainQuitButtonBack.GetComponent<Animator>().SetTrigger(ShowHash);

        yield return new WaitForSeconds(AfterButtonBackDelay);

        TransitionBehaviour.Instance.ShowAnimation();

        yield return new WaitForSeconds(TransitionBehaviour.AfterTransitionShowDelay);
        
        GameManager.Instance.QuitGame();
    }

    private IEnumerator SettingsBackToMainAnimation()
    {
        _settingsTitleAnim.SetTrigger(HideHash);
        _settingsBackButtonAnim.SetTrigger(HideHash);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(HideHash);
        }
        
        yield return new WaitForSeconds(AfterHideDelay);
        
        _mainTitleAnim.SetTrigger(ShowHash);
        _infoTextAnim.SetTrigger(ShowHash);
        _startButtonAnim.SetTrigger(ShowHash);
        _settingsButtonAnim.SetTrigger(ShowHash);
        _quitButtonAnim.SetTrigger(ShowHash);
    }

    private IEnumerator BackToMainSceneAnimation()
    {
        TransitionBehaviour.Instance.HideAnimation();
        yield return new WaitForSeconds(0.5f);
        _mainTitleAnim.SetTrigger(ShowHash);
        _infoTextAnim.SetTrigger(ShowHash);
        _startButtonAnim.SetTrigger(ShowHash);
        _settingsButtonAnim.SetTrigger(ShowHash);
        _quitButtonAnim.SetTrigger(ShowHash);
    }

    private IEnumerator VideoSettingsAnimation()
    {
        _settingsTitleAnim.SetTrigger(HideHash);
        _settingsBackButtonAnim.SetTrigger(HideHash);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(HideHash);
        }
        
        yield return new WaitForSeconds(0.2f);
        
        _videoSettingsTitleAnim.SetTrigger(ShowHash);
        
        foreach (var anim in _videoSettingsButtonAnims)
        {
            anim.SetTrigger(ShowHash);
        }

        _videoSettingsApplyButtonAnim.SetTrigger(ShowHash);
        _videoSettingsBackButtonAnim.SetTrigger(ShowHash);
    }

    private IEnumerator VideoSettingsBackToSettingsAnimation()
    {
        _videoSettingsTitleAnim.SetTrigger(HideHash);
        _videoSettingsApplyButtonAnim.SetTrigger(HideHash);
        _videoSettingsBackButtonAnim.SetTrigger(HideHash);

        foreach (var anim in _videoSettingsButtonAnims)
        {
            anim.SetTrigger(HideHash);
        }

        yield return new WaitForSeconds(AfterHideDelay);

        BackToSettingsAnimation();
    }

    private void BackToSettingsAnimation()
    {
        _settingsTitleAnim.SetTrigger(ShowHash);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(ShowHash);
        }
        
        _settingsBackButtonAnim.SetTrigger(ShowHash);
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

    public void OnVideoSettingsButtonPressed()
    {
        settingsManager.SetAllVideoSettingsText();
        StartCoroutine(VideoSettingsAnimation());
    }

    public void OnSettingsBackButtonPressed()
    {
        StartCoroutine(SettingsBackToMainAnimation());
    }
    
    public void OnVideoSettingsBackButtonPressed()
    {
        TransitionBehaviour.Instance.SetSize();
        StartCoroutine(VideoSettingsBackToSettingsAnimation());
    }
}
