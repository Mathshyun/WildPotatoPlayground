using UnityEngine;

public class TransitionBehaviour : MonoBehaviour
{
    public const float AfterTransitionShowDelay = 0.5f;
    
    private void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width + Screen.height, Screen.height);
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, 0f);
        transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height / 2f, 0f);
        transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height / 2f, 0f);
    }
}
