using UnityEngine;

public class DebugBehaviour : MonoBehaviour
{
    [SerializeField] private RectTransform targetRect;

    public int targetFrameRate;
    public float timeScale;

    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        Time.timeScale = timeScale;
    }
    private void Update()
    {
        Debug.Log(targetRect.position);
    }
}
