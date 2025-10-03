using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class UIHoverButton : UIBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsHovered { get; private set; }

    public UnityEvent onClick, onHoverStart, onHoverEnd;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsHovered)
        {
            IsHovered = true;
            onHoverStart.Invoke();
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (IsHovered)
        {
            IsHovered = false;
            onHoverEnd.Invoke();
        }
    }

    protected virtual void OnDisable()
    {
        if (IsHovered)
        {
            IsHovered = false;
            onHoverEnd.Invoke();
        }
    }
}