using UnityEngine;

public class TransitionBehaviour : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width + Screen.height / 2f, Screen.height);
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, 0f);
        transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height / 2f, 0f);
    }
}
