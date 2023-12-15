using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonAnimation : MonoBehaviour
{
    private const float HoverPosX = -40f;
    private const float MoveSpeed = 800f;
    private const float AlphaSpeed = 2f;
    
    private bool _isHovering;
    private bool _isPressed;
    private float _normalPosX;

    private Button _button;
    private Image _image;
    private RectTransform _rectTransform;

    private void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _normalPosX = _rectTransform.anchoredPosition.x;
    }

    private IEnumerator MoveToPos(float initialPosX, float finalPosX)
    {
        var increasing = initialPosX < finalPosX;
        var velocity = (increasing ? 1 : -1) * MoveSpeed * Time.deltaTime;
        
        while (increasing == _isHovering)
        {
            var currentPos = _rectTransform.anchoredPosition;
            
            currentPos.x += velocity;
            if ((increasing && currentPos.x >= finalPosX) || (!increasing && currentPos.x <= finalPosX))
            {
                currentPos.x = finalPosX;
                _rectTransform.anchoredPosition = currentPos;
                yield break;
            }
            _rectTransform.anchoredPosition = currentPos;
            yield return null;
        }
    }

    private IEnumerator ChangeAlpha(float initialAlpha, float finalAlpha)
    {
        var increasing = initialAlpha < finalAlpha;
        var velocity = (increasing ? 1 : -1) * AlphaSpeed * Time.deltaTime;
        
        while (increasing == _isPressed)
        {
            var currentAlpha = _image.color.a;
            
            currentAlpha += velocity;
            if ((increasing && currentAlpha >= finalAlpha) || (!increasing && currentAlpha <= finalAlpha))
            {
                currentAlpha = finalAlpha;
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, currentAlpha);
                yield break;
            }
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, currentAlpha);
            yield return null;
        }
    }
    
    public void OnPointerEnter()
    {
        if (_button.interactable == false) return;
        _isHovering = true;
        StartCoroutine(MoveToPos(_normalPosX, HoverPosX));
    }
    
    public void OnPointerExit()
    {
        if (_button.interactable == false) return;
        _isHovering = false;
        StartCoroutine(MoveToPos(HoverPosX, _normalPosX));
    }
    
    public void OnPointerDown()
    {
        if (_button.interactable == false) return;
        _isPressed = true;
        StartCoroutine(ChangeAlpha(0.5f, 1f));
    }
    
    public void OnPointerUp()
    {
        if (_button.interactable == false) return;
        _isPressed = false;
        StartCoroutine(ChangeAlpha(1f, 0.5f));
    }
}
