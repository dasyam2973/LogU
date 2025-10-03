using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggableObject : UIBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _handleRect;

    public bool IsDragging { get; protected set; }

    private Vector2 _draggingOffset;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!IsDragging && RectTransformUtility.RectangleContainsScreenPoint(_handleRect, eventData.position, null))
        {
            var canvasRect = MainController.Instance.PopupCanvas.transform as RectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out Vector2 localPoint);
            _draggingOffset = RectTransform.anchoredPosition - localPoint;
            IsDragging = true;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (IsDragging)
        {
            IsDragging = false;
        }
    }

    protected virtual void OnDisable()
    {
        if (IsDragging)
        {
            IsDragging = false;
        }
    }

    protected virtual void Update()
    {
        if (IsDragging)
        {
            var canvasRect = MainController.Instance.PopupCanvas.transform as RectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out Vector2 localPoint);
            RectTransform.anchoredPosition = localPoint + _draggingOffset;
            RectTransform.ClampPositionToBounds(canvasRect);
            RectTransform.anchoredPosition = new(Mathf.Round(RectTransform.anchoredPosition.x), Mathf.Round(RectTransform.anchoredPosition.y));
        }
    }
}