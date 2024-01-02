using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum SceneIndex
    {
        Main = 0,
        Start = 1
    }
    
    public static GameManager Instance { get; private set; }

    public bool nothing;
    public bool anything;
    public bool everything;
    public bool oneMoreThing;
    public bool isMainAnimationFinished;

    public readonly List<Resolution> Resolutions = new();
    
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
        var currentRefreshRate = Screen.currentResolution.refreshRateRatio;
        
        foreach (var resolution in Screen.resolutions)
        {
            if (resolution.refreshRateRatio.Equals(currentRefreshRate))
            {
                Resolutions.Add(resolution);
            }
        }
        
        nothing = false;
        anything = false;
        everything = false;
        oneMoreThing = false;
        isMainAnimationFinished = false;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene((int) SceneIndex.Main);
    }
    
    public void LoadStart()
    {
        SceneManager.LoadScene((int) SceneIndex.Start);
        isMainAnimationFinished = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
