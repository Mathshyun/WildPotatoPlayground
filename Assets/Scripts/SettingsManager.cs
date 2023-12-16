using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject somethingButton;
    [SerializeField] private GameObject nothingButton;
    [SerializeField] private GameObject anythingButton;
    [SerializeField] private GameObject everythingButton;
    [SerializeField] private GameObject oneMoreThingButton;
    
    private static bool Something
    {
        get => GameManager.Instance.something;
        set => GameManager.Instance.something = value;
    }
    
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
    
    public void SetSomething()
    {
        Something = !Something;
        somethingButton.transform.GetChild(1).GetComponent<Text>().text = Something ? "ON" : "OFF";
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
}
