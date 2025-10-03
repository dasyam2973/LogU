using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UICheckBox : UIHoverButton
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _hoverSprite;
    [SerializeField] private Image _checkImage;

    private Image _image;

    public UnityEvent<bool> onValueChanged;

    private bool _checked;
    public bool Checked
    {
        get => _checked;
        set
        {
            _checked = value;
            _checkImage.gameObject.SetActive(value);
            onValueChanged.Invoke(value);
        }
    }

    private void Awake()
    {
        _image = GetComponent<Image>();

        onHoverStart.AddListener(OnHoverStart);
        onHoverEnd.AddListener(OnHoverEnd);
        onClick.AddListener(OnClick);
    }

    private void OnHoverStart()
    {
        _image.sprite = _hoverSprite;
    }

    private void OnHoverEnd()
    {
        _image.sprite = _defaultSprite;
    }

    private void OnClick()
    {
        Checked ^= true;
    }
}