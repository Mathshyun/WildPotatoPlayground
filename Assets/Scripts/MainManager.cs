using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    private const float ButtonHideAnimationDuration = 0.2f;
    private const float ButtonBackShowAnimationDuration = 0.3f;
    private const float InfoTextShowAnimationDuration = 0.3f;
    private const float TitleTextHideAnimationDuration = 0.3f;
    private const float TransitionDuration = 0.5f;
    
    [Header ("Canvas Objects")]
    [SerializeField] private GameObject titleText1;
    [SerializeField] private GameObject titleText2;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject buttonBack;
    [SerializeField] private GameObject infoText;
    [SerializeField] private GameObject transition;
    
    [Header ("Prefabs")]
    [SerializeField] private GameObject startButtonBackPrefab;
    [SerializeField] private GameObject settingsButtonBackPrefab;
    [SerializeField] private GameObject quitButtonBackPrefab;

    private static float ButtonBackShowAnimationMethod(float x) => Mathf.Pow(x, 2) - 2 * x + 1f;


    private void Awake()
    {
        transition.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Screen.width + Screen.height / 2f, Screen.height);
        transition.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta =
            new Vector2(Screen.width, 0);
        transition.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta =
            new Vector2(Screen.height / 2f, 0);
    }
    
    private void Start()
    {
        titleText1.SetActive(false);
        titleText2.SetActive(false);
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
        infoText.SetActive(false);
        infoText.GetComponent<Text>().text = $"v{Application.version}";
        StartCoroutine(PlayIntroAnimation());
    }

    private IEnumerator PlayIntroAnimation()
    {
        var titleText1Anim = titleText1.GetComponent<Animator>();
        var titleText2Anim = titleText2.GetComponent<Animator>();
        var startButtonAnim = startButton.GetComponent<Animator>();
        var settingsButtonAnim = settingsButton.GetComponent<Animator>();
        var quitButtonAnim = quitButton.GetComponent<Animator>();
        
        titleText1.SetActive(true);
        titleText1Anim.Play("Show");
        yield return new WaitForSeconds(0.2f);
        titleText2.SetActive(true);
        titleText2Anim.Play("Show");
        yield return new WaitForSeconds(0.8f);
        
        titleText1Anim.Play("Move Top");
        titleText2Anim.Play("Move Top");
        yield return new WaitForSeconds(0.5f);
        
        startButton.SetActive(true);
        startButtonAnim.Play("Show");
        yield return new WaitForSeconds(0.2f);
        
        settingsButton.SetActive(true);
        settingsButtonAnim.Play("Show");
        yield return new WaitForSeconds(0.2f);
        
        quitButton.SetActive(true);
        quitButtonAnim.Play("Show");
        yield return new WaitForSeconds(0.2f);

        infoText.SetActive(true);
        StartCoroutine(MainInfoTextShowAnimation());

        yield return new WaitForSeconds(0.3f);
        
        startButton.GetComponent<Button>().interactable = true;
        settingsButton.GetComponent<Button>().interactable = true;
        quitButton.GetComponent<Button>().interactable = true;
        titleText1Anim.enabled = false;
        titleText2Anim.enabled = false;
        startButtonAnim.enabled = false;
        settingsButtonAnim.enabled = false;
        quitButtonAnim.enabled = false;
    }

    private IEnumerator StartButtonPressed()
    {
        var startButtonBack = Instantiate(startButtonBackPrefab, buttonBack.transform);
        
        StartCoroutine(MainButtonHideAnimation(settingsButton.GetComponent<RectTransform>()));
        StartCoroutine(MainButtonHideAnimation(quitButton.GetComponent<RectTransform>()));
        
        yield return StartCoroutine(MainButtonBackShowAnimation(startButtonBack.GetComponent<RectTransform>()));
        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(TransitionShowAnimation());
        yield return new WaitForSeconds(0.3f);

        // To be removed later
        GameManager.QuitGame();
    }
    
    private IEnumerator SettingsButtonPressed()
    {
        var settingsButtonBack = Instantiate(settingsButtonBackPrefab, buttonBack.transform);
        
        StartCoroutine(MainButtonHideAnimation(startButton.GetComponent<RectTransform>()));
        StartCoroutine(MainButtonHideAnimation(quitButton.GetComponent<RectTransform>()));
        StartCoroutine(TitleTextHideAnimation());
        
        yield return StartCoroutine(MainButtonBackShowAnimation(settingsButtonBack.GetComponent<RectTransform>()));
        yield return new WaitForSeconds(0.3f);
        
        // To be removed later
        yield return StartCoroutine(TransitionShowAnimation());
        yield return new WaitForSeconds(0.3f);

        GameManager.QuitGame();
    }
    
    private IEnumerator QuitButtonPressed()
    {
        var quitButtonBack = Instantiate(quitButtonBackPrefab, buttonBack.transform);
        
        StartCoroutine(MainButtonHideAnimation(startButton.GetComponent<RectTransform>()));
        StartCoroutine(MainButtonHideAnimation(settingsButton.GetComponent<RectTransform>()));
        
        yield return StartCoroutine(MainButtonBackShowAnimation(quitButtonBack.GetComponent<RectTransform>()));
        yield return new WaitForSeconds(0.3f);
        
        yield return StartCoroutine(TransitionShowAnimation());
        yield return new WaitForSeconds(0.3f);

        GameManager.QuitGame();
    }

    private IEnumerator TitleTextHideAnimation()
    {
        var startTime = Time.time;
        var titleText1Rect = titleText1.GetComponent<RectTransform>();
        var titleText2Rect = titleText2.GetComponent<RectTransform>();

        titleText1Rect.anchorMin = new Vector2(0.5f, 0.5f);
        titleText1Rect.anchorMax = new Vector2(0.5f, 0.5f);
        titleText1Rect.pivot = new Vector2(0.5f, 0.5f);
        titleText2Rect.anchorMin = new Vector2(0.5f, 0.5f);
        titleText2Rect.anchorMax = new Vector2(0.5f, 0.5f);
        titleText2Rect.pivot = new Vector2(0.5f, 0.5f);

        while (true)
        {
            var progress = (Time.time - startTime) / TitleTextHideAnimationDuration;

            if (progress >= 1f) break;

            progress *= progress;
            
            titleText1Rect.anchorMin = new Vector2(0.5f - progress * 0.5f, 0.5f);
            titleText1Rect.anchorMax = new Vector2(0.5f - progress * 0.5f, 0.5f);
            titleText1Rect.pivot = new Vector2(0.5f + progress * 0.5f, 0.5f);
            titleText2Rect.anchorMin = new Vector2(0.5f - progress * 0.5f, 0.5f);
            titleText2Rect.anchorMax = new Vector2(0.5f - progress * 0.5f, 0.5f);
            titleText2Rect.pivot = new Vector2(0.5f + progress * 0.5f, 0.5f);

            yield return null;
        }
        
        titleText1Rect.anchorMin = new Vector2(0f, 0.5f);
        titleText1Rect.anchorMax = new Vector2(0f, 0.5f);
        titleText1Rect.pivot = new Vector2(1f, 0.5f);
        titleText2Rect.anchorMin = new Vector2(0f, 0.5f);
        titleText2Rect.anchorMax = new Vector2(0f, 0.5f);
        titleText2Rect.pivot = new Vector2(1f, 0.5f);
    }

    private IEnumerator MainInfoTextShowAnimation()
    {
        var startTime = Time.time;
        var infoTextRect = infoText.GetComponent<RectTransform>();

        infoTextRect.pivot = new Vector2(0f, 0f);

        while (Time.time - startTime < InfoTextShowAnimationDuration)
        {
            var progress = (Time.time - startTime) / InfoTextShowAnimationDuration;
            infoTextRect.pivot = new Vector2(progress * (2f - progress), 0f);
            yield return null;
        }
        
        infoTextRect.pivot = new Vector2(1f, 0f);
    }

    private IEnumerator TransitionShowAnimation()
    {
        var startTime = Time.time;
        var transitionRect = transition.GetComponent<RectTransform>();
        
        transitionRect.pivot = new Vector2(1f, 0.5f);
        transition.SetActive(true);

        while (true)
        {
            var progress = (Time.time - startTime) / TransitionDuration;

            if (progress >= 1f) break;
            
            transitionRect.pivot = new Vector2(progress * (progress - 2f) + 1f, 0.5f);
            yield return null;
        }
        
        transitionRect.pivot = new Vector2(0f, 0.5f);
    }

    private static IEnumerator MainButtonHideAnimation(RectTransform buttonRect)
    {
        var startTime = Time.time;
        
        while (Time.time - startTime < ButtonHideAnimationDuration)
        {
            var currentPivotX = Mathf.Pow((Time.time - startTime) / ButtonHideAnimationDuration, 2f);
            buttonRect.pivot = new Vector2(currentPivotX, buttonRect.pivot.y);
            yield return null;
        }
        
        buttonRect.pivot = new Vector2(1, buttonRect.pivot.y);
    }

    private static IEnumerator MainButtonBackShowAnimation(RectTransform backRect)
    {
        var startTime = Time.time;

        backRect.pivot = new Vector2(1f, backRect.pivot.y);
        backRect.gameObject.SetActive(true);
        
        while (Time.time - startTime < ButtonBackShowAnimationDuration)
        {
            var currentPivotX = ButtonBackShowAnimationMethod((Time.time - startTime) / ButtonBackShowAnimationDuration);
            backRect.pivot = new Vector2(currentPivotX, backRect.pivot.y);
            yield return null;
        }
        
        backRect.pivot = new Vector2(0, backRect.pivot.y);
    }

    private void DisableButtons()
    {
        startButton.GetComponent<Button>().interactable = false;
        settingsButton.GetComponent<Button>().interactable = false;
        quitButton.GetComponent<Button>().interactable = false;
    }
    
    public void OnStartButtonPressed()
    {
        DisableButtons();
        StartCoroutine(StartButtonPressed());
    }
    
    public void OnSettingsButtonPressed()
    {
        DisableButtons();
        StartCoroutine(SettingsButtonPressed());
    }
    
    public void OnQuitButtonPressed()
    {
        DisableButtons();
        StartCoroutine(QuitButtonPressed());
    }
}
