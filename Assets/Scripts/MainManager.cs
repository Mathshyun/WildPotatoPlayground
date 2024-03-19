using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MainStates
{
    Main,
    Settings,
    VideoSettings
}

public class MainManager : MonoBehaviour
{
    private const float AfterButtonBackDelay = 0.5f;
    private const float AfterHideDelay = 0.2f;
    private const float SettingsShowDelay = 0.1f;
    
    private static readonly int FirstHash = Animator.StringToHash("First");
    private static readonly int ShowHash = Animator.StringToHash("Show");
    private static readonly int HideHash = Animator.StringToHash("Hide");
    private static readonly int SkipHash = Animator.StringToHash("Skip");
    
    public MainStates state;
    
    [Header ("Main")]
    [SerializeField] private GameObject mainFirstSelected;
    [SerializeField] private Text mainInfoText;

    [Space(10)]
    [SerializeField] private Animator mainTitleAnim;
    [SerializeField] private Animator mainInfoAnim;
    
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
    [SerializeField] private GameObject settingsButtons;
    [SerializeField] private GameObject settingsFirstSelected;
    [SerializeField] private Button settingsBackButton;
    
    [Space (10)]
    [SerializeField] private Animator settingsTitleAnim;
    [SerializeField] private Animator settingsBackButtonAnim;

    [Header("Video Settings")]
    [SerializeField] private GameObject videoSettingsButtons;
    [SerializeField] private GameObject videoSettingsFirstSelected;
    [SerializeField] private Button videoSettingsBackButton;

    [Space(10)]
    [SerializeField] private Animator videoSettingsTitleAnim;
    [SerializeField] private Animator videoSettingsApplyButtonAnim;
    [SerializeField] private Animator videoSettingsBackButtonAnim;

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
        state = MainStates.Main;
        
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
            _firstAnimation = EnterMain();
            StartCoroutine(_firstAnimation);
        }
        else
        {
            StartCoroutine(ReturnMain());
        }
        
        settingsManager.SetAllSettingsText();
        settingsManager.SetAllVideoSettingsText();
    }
    
    private IEnumerator EnterMain()
    {
        mainTitleAnim.SetTrigger(FirstHash);
        
        yield return new WaitForSeconds(1.5f);
        
        mainStartButtonAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        mainSettingsButtonAnim.SetTrigger(ShowHash);
        yield return new WaitForSeconds(0.2f);
        mainQuitButtonAnim.SetTrigger(ShowHash);
        mainInfoAnim.SetTrigger(ShowHash);
        
        yield return new WaitUntil(() => mainInfoAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"));

        IsMainAnimationFinished = true;
    }

    private IEnumerator EnterStart()
    {
        mainStartButtonAnim.SetTrigger(HideHash);
        mainSettingsButtonAnim.SetTrigger(HideHash);
        mainQuitButtonAnim.SetTrigger(HideHash);
        
        mainStartButtonBackAnim.SetTrigger(ShowHash);

        yield return new WaitForSeconds(AfterButtonBackDelay);

        TransitionBehaviour.Instance.ShowAnimation();
        
        yield return new WaitUntil(() => TransitionBehaviour.Instance.IsTransitionFinished());

        GameManager.Instance.LoadStart();
    }
    
    private IEnumerator EnterSettings()
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

    private IEnumerator EnterQuit()
    {
        mainStartButtonAnim.SetTrigger(HideHash);
        mainSettingsButtonAnim.SetTrigger(HideHash);
        mainQuitButtonAnim.SetTrigger(HideHash);
        
        mainQuitButtonBackAnim.SetTrigger(ShowHash);

        yield return new WaitForSeconds(AfterButtonBackDelay);

        TransitionBehaviour.Instance.ShowAnimation();

        yield return new WaitUntil(() => TransitionBehaviour.Instance.IsTransitionFinished());
        
        GameManager.Instance.QuitGame();
    }

    private IEnumerator ExitSettings()
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

    private IEnumerator ReturnMain()
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
        StartCoroutine(EnterStart());
    }
    
    public void OnSettingsButtonPressed()
    {
        state = MainStates.Settings;
        StartCoroutine(EnterSettings());
    }
    
    public void OnQuitButtonPressed()
    {
        StartCoroutine(EnterQuit());
    }

    public void OnVideoSettingsButtonPressed()
    {
        state = MainStates.VideoSettings;
        settingsManager.SetAllVideoSettingsText();
        StartCoroutine(VideoSettingsAnimation());
    }

    public void OnSettingsBackButtonPressed()
    {
        state = MainStates.Main;
        StartCoroutine(ExitSettings());
    }
    
    public void OnVideoSettingsBackButtonPressed()
    {
        state = MainStates.Settings;
        TransitionBehaviour.Instance.SetSize();
        StartCoroutine(VideoSettingsBackToSettingsAnimation());
    }

    public void OnAnyKey()
    {
        if (IsMainAnimationFinished) return;
        
        IsMainAnimationFinished = true;
        
        StopCoroutine(_firstAnimation);
    
        mainTitleAnim.SetTrigger(SkipHash);
        mainInfoAnim.SetTrigger(SkipHash);
        mainStartButtonAnim.SetTrigger(SkipHash);
        mainSettingsButtonAnim.SetTrigger(SkipHash);
        mainQuitButtonAnim.SetTrigger(SkipHash);
    }

    public void OnCancel()
    {
        switch (state)
        {
            case MainStates.Main:
                break;
            
            case MainStates.Settings:
                if (settingsBackButton.IsInteractable()) OnSettingsBackButtonPressed();
                break;

            case MainStates.VideoSettings:
                if (videoSettingsBackButton.IsInteractable()) OnVideoSettingsBackButtonPressed();
                break;
            
            default:
                throw new ArgumentException();
        }
    }
}
