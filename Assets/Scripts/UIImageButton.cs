using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIImageButton : UIHoverButton
{
    public enum HoverEventType
    {
        ColorTint,
        SpriteChange
    }

    [SerializeField] private HoverEventType _type;

    [Header("Color Tint")]
    [SerializeField] private Color _endColor;
    private Color _defaultColor;

    [Header("Sprite Change")]
    [SerializeField] private Sprite _sprite;
    private Sprite _defaultSprite;

    [Header("Tooltip")]
    public bool useTooltip;
    [SerializeField] private UITooltip _tooltip;
    [SerializeField] private float _tooltipHoverDelay;

    private Image _image;

    private float _hoverTime;

    private void Awake()
    {
        _image = GetComponent<Image>();
        if (_type == HoverEventType.ColorTint)
        {
            _defaultColor = _image.color;
            onHoverStart.AddListener(() =>
            {
                _image.color = _endColor;
            });
            onHoverEnd.AddListener(() =>
            {
                _image.color = _defaultColor;
            });
        }
        else if (_type == HoverEventType.SpriteChange)
        {
            _defaultSprite = _image.sprite;
            onHoverStart.AddListener(() =>
            {
                _image.sprite = _sprite;
            });
            onHoverEnd.AddListener(() =>
            {
                _image.sprite = _defaultSprite;
            });
        }
        if (useTooltip && !_tooltip)
            Debug.LogWarning("Use Tooltip is enabled, but the object does not have a tooltip.");
    }

    private void Update()
    {
        if (IsHovered)
        {
            _hoverTime += Time.deltaTime;
            if (useTooltip && _tooltip && _hoverTime >= _tooltipHoverDelay)
                _tooltip.Show();
            else if (_tooltip)
                _tooltip.Hide();
        }
        else
        {
            _hoverTime = 0f;
            if (_tooltip)
                _tooltip.Hide();
        }
    }
}