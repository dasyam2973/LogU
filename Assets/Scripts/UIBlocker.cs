using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIBlocker : UIBehaviour, IPointerClickHandler
{
    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke();
    }
}