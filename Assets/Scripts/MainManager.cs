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
    [SerializeField] private Animator mainTitleAnim;
    [SerializeField] private Animator mainInfoAnim;
    [SerializeField] private Text mainInfoText;
    
    [Space (10)]
    [SerializeField] private Animator mainStartButtonAnim;
    [SerializeField] private Animator mainSettingsButtonAnim;
    [SerializeField] private Animator mainQuitButtonAnim;
    
    [Space (10)]
    [SerializeField] private Animator mainStartButtonBackAnim;
    [SerializeField] private Animator mainSettingsButtonBackAnim;
    [SerializeField] private Animator mainQuitButtonBackAnim;

    [Header("Settings")]
    [SerializeField] private SettingsManager settingsManager;
    
    [Space (10)]
    [SerializeField] private Animator settingsTitleAnim;
    [SerializeField] private Animator settingsBackButtonAnim;
    [SerializeField] private GameObject settingsButtons;

    [Header("Video Settings")]
    [SerializeField] private Animator videoSettingsTitleAnim;
    [SerializeField] private Animator videoSettingsApplyButtonAnim;
    [SerializeField] private Animator videoSettingsBackButtonAnim;
    [SerializeField] private GameObject videoSettingsButtons;

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
        mainInfoText.text = $"v{Application.version}";
        
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
        
            mainTitleAnim.SetTrigger(SkipHash);
            mainInfoAnim.SetTrigger(SkipHash);
            mainStartButtonAnim.SetTrigger(SkipHash);
            mainSettingsButtonAnim.SetTrigger(SkipHash);
            mainQuitButtonAnim.SetTrigger(SkipHash);
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
        mainTitleAnim.SetTrigger(FirstHash);
        
        yield return new WaitForSeconds(1.5f);
        
        mainStartButtonAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        mainSettingsButtonAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        mainQuitButtonAnim.SetTrigger(ShowHash);
        mainInfoAnim.SetTrigger(ShowHash);
        
        yield return new WaitForSeconds(0.5f);
        
        IsMainAnimationFinished = true;
    }

    private IEnumerator StartAnimation()
    {
        mainStartButtonAnim.SetTrigger(HideHash);
        mainSettingsButtonAnim.SetTrigger(HideHash);
        mainQuitButtonAnim.SetTrigger(HideHash);
        
        mainStartButtonBackAnim.SetTrigger(ShowHash);

        yield return new WaitForSeconds(AfterButtonBackDelay);

        TransitionBehaviour.Instance.ShowAnimation();
        
        yield return new WaitForSeconds(TransitionBehaviour.AfterTransitionShowDelay);

        GameManager.Instance.LoadStart();
    }
    
    private IEnumerator SettingsAnimation()
    {
        mainStartButtonAnim.SetTrigger(HideHash);
        mainSettingsButtonAnim.SetTrigger(HideHash);
        mainQuitButtonAnim.SetTrigger(HideHash);
        
       mainSettingsButtonBackAnim.SetTrigger(ShowHash);
        mainTitleAnim.SetTrigger(HideHash);
        mainInfoAnim.SetTrigger(HideHash);
        
        yield return new WaitForSeconds(AfterButtonBackDelay);
        
       mainSettingsButtonBackAnim.SetTrigger(HideHash);

        yield return new WaitForSeconds(AfterHideDelay);
        
        settingsTitleAnim.SetTrigger(ShowHash);
        
        yield return new WaitForSeconds(SettingsShowDelay);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(ShowHash);
            yield return new WaitForSeconds(SettingsShowDelay);
        }

        settingsBackButtonAnim.SetTrigger(ShowHash);
    }

    private IEnumerator QuitAnimation()
    {
        mainStartButtonAnim.SetTrigger(HideHash);
        mainSettingsButtonAnim.SetTrigger(HideHash);
        mainQuitButtonAnim.SetTrigger(HideHash);
        
        mainQuitButtonBackAnim.SetTrigger(ShowHash);

        yield return new WaitForSeconds(AfterButtonBackDelay);

        TransitionBehaviour.Instance.ShowAnimation();

        yield return new WaitForSeconds(TransitionBehaviour.AfterTransitionShowDelay);
        
        GameManager.Instance.QuitGame();
    }

    private IEnumerator SettingsBackToMainAnimation()
    {
        settingsTitleAnim.SetTrigger(HideHash);
        settingsBackButtonAnim.SetTrigger(HideHash);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(HideHash);
        }
        
        yield return new WaitForSeconds(AfterHideDelay);
        
        mainTitleAnim.SetTrigger(ShowHash);
        mainInfoAnim.SetTrigger(ShowHash);
        mainStartButtonAnim.SetTrigger(ShowHash);
        mainSettingsButtonAnim.SetTrigger(ShowHash);
        mainQuitButtonAnim.SetTrigger(ShowHash);
    }

    private IEnumerator BackToMainSceneAnimation()
    {
        TransitionBehaviour.Instance.HideAnimation();
        yield return new WaitForSeconds(0.5f);
        mainTitleAnim.SetTrigger(ShowHash);
        mainInfoAnim.SetTrigger(ShowHash);
        mainStartButtonAnim.SetTrigger(ShowHash);
        mainSettingsButtonAnim.SetTrigger(ShowHash);
        mainQuitButtonAnim.SetTrigger(ShowHash);
    }

    private IEnumerator VideoSettingsAnimation()
    {
        settingsTitleAnim.SetTrigger(HideHash);
        settingsBackButtonAnim.SetTrigger(HideHash);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(HideHash);
        }
        
        yield return new WaitForSeconds(0.2f);
        
        videoSettingsTitleAnim.SetTrigger(ShowHash);
        
        foreach (var anim in _videoSettingsButtonAnims)
        {
            anim.SetTrigger(ShowHash);
        }

        videoSettingsApplyButtonAnim.SetTrigger(ShowHash);
        videoSettingsBackButtonAnim.SetTrigger(ShowHash);
    }

    private IEnumerator VideoSettingsBackToSettingsAnimation()
    {
        videoSettingsTitleAnim.SetTrigger(HideHash);
        videoSettingsApplyButtonAnim.SetTrigger(HideHash);
        videoSettingsBackButtonAnim.SetTrigger(HideHash);

        foreach (var anim in _videoSettingsButtonAnims)
        {
            anim.SetTrigger(HideHash);
        }

        yield return new WaitForSeconds(AfterHideDelay);

        BackToSettingsAnimation();
    }

    private void BackToSettingsAnimation()
    {
        settingsTitleAnim.SetTrigger(ShowHash);
        
        foreach (var anim in _settingsButtonAnims)
        {
            anim.SetTrigger(ShowHash);
        }
        
        settingsBackButtonAnim.SetTrigger(ShowHash);
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
