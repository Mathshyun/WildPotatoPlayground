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

    public bool something;
    public bool nothing;
    public bool anything;
    public bool everything;
    public bool oneMoreThing;
    public bool isMainAnimationFinished;
    
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
        something = false;
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
