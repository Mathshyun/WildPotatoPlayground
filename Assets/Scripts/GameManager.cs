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

    public bool nothing;
    public bool anything;
    public bool everything;
    public bool oneMoreThing;
    public bool isMainAnimationFinished;

    public readonly List<Resolution> Resolutions = new();
    
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
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
        
        Application.targetFrameRate = (int) Screen.currentResolution.refreshRateRatio.value;
        
        foreach (var resolution in Screen.resolutions)
        {
            if (resolution.refreshRateRatio.Equals(currentRefreshRate))
            {
                Resolutions.Add(new Resolution { width = resolution.width, height = resolution.height });
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
        isMainAnimationFinished = true;
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

    public void LoadScene(string sceneName)
    {
        // Some validation process will be added here...
        
        SceneManager.LoadScene(sceneName);
    }
}
