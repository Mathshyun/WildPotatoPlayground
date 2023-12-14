using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject titleText1;
    [SerializeField] private GameObject titleText2;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject quitButton;

    private void Start()
    {
        titleText1.SetActive(false);
        titleText2.SetActive(false);
        playButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
        StartCoroutine(PlayTitleAnimation());
    }

    private IEnumerator PlayTitleAnimation()
    {
        var titleText1Anim = titleText1.GetComponent<Animator>();
        var titleText2Anim = titleText2.GetComponent<Animator>();
        var playButtonAnim = playButton.GetComponent<Animator>();
        var settingsButtonAnim = settingsButton.GetComponent<Animator>();
        var quitButtonAnim = quitButton.GetComponent<Animator>();
        
        titleText1.SetActive(true);
        titleText1Anim.Play("Show");
        yield return new WaitForSeconds(0.3f);
        titleText2.SetActive(true);
        titleText2Anim.Play("Show");
        yield return new WaitForSeconds(1.5f);
        
        titleText1Anim.Play("Move Top");
        titleText2Anim.Play("Move Top");
        yield return new WaitForSeconds(0.5f);
        
        playButton.SetActive(true);
        playButtonAnim.Play("Show");
        yield return new WaitForSeconds(0.2f);
        
        settingsButton.SetActive(true);
        settingsButtonAnim.Play("Show");
        yield return new WaitForSeconds(0.2f);
        
        quitButton.SetActive(true);
        quitButtonAnim.Play("Show");
        yield return new WaitForSeconds(0.5f);
        
        playButton.GetComponent<Button>().interactable = true;
        settingsButton.GetComponent<Button>().interactable = true;
        quitButton.GetComponent<Button>().interactable = true;
        playButtonAnim.enabled = false;
        settingsButtonAnim.enabled = false;
        quitButtonAnim.enabled = false;
    }
}
