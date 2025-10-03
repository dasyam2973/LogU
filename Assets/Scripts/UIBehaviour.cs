using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public abstract class UIBehaviour : MonoBehaviour
{
    private RectTransform _rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
                _rectTransform = transform as RectTransform;
            return _rectTransform;
        }
    }

    public Vector2 RectSize => RectTransform.rect.size;
    public float Width => RectTransform.rect.width;
    public float Height => RectTransform.rect.height;
    public Vector2 Pivot => RectTransform.pivot;
}