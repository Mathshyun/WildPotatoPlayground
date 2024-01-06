using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header ("Settings")]
    [SerializeField] private GameObject nothingButton;
    [SerializeField] private GameObject anythingButton;
    [SerializeField] private GameObject everythingButton;
    [SerializeField] private GameObject oneMoreThingButton;
    
    [Header ("Video Settings")]
    [SerializeField] private GameObject screenModeButton;
    [SerializeField] private GameObject resolutionButton;
    
    private FullScreenMode _fullScreenMode;
    private Resolution _resolution;
    
    private static bool Nothing
    {
        get => GameManager.Instance.nothing;
        set => GameManager.Instance.nothing = value;
    }
    
    private static bool Anything
    {
        get => GameManager.Instance.anything;
        set => GameManager.Instance.anything = value;
    }
    
    private static bool Everything
    {
        get => GameManager.Instance.everything;
        set => GameManager.Instance.everything = value;
    }
    
    private static bool OneMoreThing
    {
        get => GameManager.Instance.oneMoreThing;
        set => GameManager.Instance.oneMoreThing = value;
    }
    
#region Settings
    
    public void SetAllSettingsText()
    {
        nothingButton.transform.GetChild(1).GetComponent<Text>().text = Nothing ? "ON" : "OFF";
        anythingButton.transform.GetChild(1).GetComponent<Text>().text = Anything ? "ON" : "OFF";
        everythingButton.transform.GetChild(1).GetComponent<Text>().text = Everything ? "ON" : "OFF";
        oneMoreThingButton.transform.GetChild(1).GetComponent<Text>().text = OneMoreThing ? "ON" : "OFF";
    }
    
    public void SetNothing()
    {
        Nothing = !Nothing;
        nothingButton.transform.GetChild(1).GetComponent<Text>().text = Nothing ? "ON" : "OFF";
    }
    
    public void SetAnything()
    {
        Anything = !Anything;
        anythingButton.transform.GetChild(1).GetComponent<Text>().text = Anything ? "ON" : "OFF";
    }
    
    public void SetEverything()
    {
        Everything = !Everything;
        everythingButton.transform.GetChild(1).GetComponent<Text>().text = Everything ? "ON" : "OFF";
    }
    
    public void SetOneMoreThing()
    {
        OneMoreThing = !OneMoreThing;
        oneMoreThingButton.transform.GetChild(1).GetComponent<Text>().text = OneMoreThing ? "ON" : "OFF";
    }
    
#endregion
    
#region Video Settings

    private void SetFullScreenModeText()
    {
        screenModeButton.transform.GetChild(1).GetComponent<Text>().text = _fullScreenMode switch
        {
            FullScreenMode.ExclusiveFullScreen => "Full Screen",
            FullScreenMode.MaximizedWindow     => "Full Screen",
            FullScreenMode.FullScreenWindow    => "Borderless",
            FullScreenMode.Windowed            => "Windowed",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void SetResolutionText()
    {
        resolutionButton.transform.GetChild(1).GetComponent<Text>().text = $"{_resolution.width}x{_resolution.height}";
    }
    
    public void SetAllVideoSettingsText()
    {
        _fullScreenMode = Screen.fullScreenMode;
        _resolution = new Resolution { width = Screen.width, height = Screen.height };
        SetFullScreenModeText();
        SetResolutionText();
    }

    public void SetFullScreenMode()
    {
#if !UNITY_STANDALONE_WIN && !UNITY_STANDALONE_OSX
        return;
#endif

        switch (_fullScreenMode)
        {
            case FullScreenMode.ExclusiveFullScreen:
            case FullScreenMode.MaximizedWindow:
                _fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case FullScreenMode.FullScreenWindow:
                _fullScreenMode = FullScreenMode.Windowed;
                break;
            case FullScreenMode.Windowed:
#if UNITY_STANDALONE_WIN
                _fullScreenMode = FullScreenMode.ExclusiveFullScreen;
#elif UNITY_STANDALONE_OSX
                fullScreenMode = FullScreenMode.MaximizedWindow;
#endif
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        SetFullScreenModeText();
    }

    public void SetResolution()
    {
        var resolutions = GameManager.Instance.Resolutions;
        var index = resolutions.IndexOf(_resolution);
        
        index = (index + 1) % resolutions.Count;
        _resolution = resolutions[index];
        
        SetResolutionText();
    }

    public void ApplyVideoSettings()
    {
        Screen.SetResolution(_resolution.width, _resolution.height, _fullScreenMode);
    }
    
#endregion
}
