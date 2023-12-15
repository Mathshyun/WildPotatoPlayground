using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private static readonly int HoverHash = Animator.StringToHash("Hover");
    private static readonly int PressedHash = Animator.StringToHash("Pressed");
    
    private Button _button;
    private Animator _anim;

    private void Start()
    {
        _button = GetComponent<Button>();
        _anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable) _anim.SetBool(HoverHash, true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button.interactable) _anim.SetBool(HoverHash, false);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable) _anim.SetBool(PressedHash, true);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_button.interactable) _anim.SetBool(PressedHash, false);
    }
}
