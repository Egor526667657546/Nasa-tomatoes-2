using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRegister : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;

    public bool IsHovered { get; private set; } = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovered = false;
    }

    public void PressButton()
    {
        if (button != null && button.interactable)
        {
            Debug.Log(button.name);
            IsHovered = false;
            button.onClick.Invoke();
        }
    }
}
