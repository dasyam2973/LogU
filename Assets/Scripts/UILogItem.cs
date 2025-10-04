using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UILogItem : UIBehaviour, IPointerClickHandler
{
    public enum HighlightType
    {
        New,
        Search,
        Click
    }

    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _dateTimeText;
    [SerializeField] private TextMeshProUGUI _levelText;
    private Image _image;

    [Header("Highlight")]
    [SerializeField] private Color _highlightColor_New;
    [SerializeField] private float _highlightDuration_New;
    [SerializeField] private Color _highlightColor_Search;
    [SerializeField] private float _highlightDuration_Search;
    [SerializeField] private Color _highlightColor_Click;
    [SerializeField] private float _highlightDuration_Click;

    private LogViewModel _viewModel;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (_viewModel != null)
        {
            _viewModel.Update(Time.time);
            _image.color = GetHighlightColor(_viewModel.highlightType).WithAlpha(1f - Mathf.Clamp01(_viewModel.highlightElapsed / GetHighlightDuration(_viewModel.highlightType)));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GUIUtility.systemCopyBuffer = _viewModel.log.message;
            MainController.Instance.OnLogCopiedToClipboard();
            SetHighlight(HighlightType.Click);
        }
    }

    public void SetViewModel(LogViewModel model)
    {
        _viewModel = model;
        _viewModel.Update(Time.time);
        _messageText.text = _viewModel.log.message;
        _dateTimeText.text = _viewModel.log.dateTime.ToString("[HH:mm:ss]");
        _levelText.text = _viewModel.log.level.ToRichTextString();
        RefreshLayout();
        Update();
    }

    public void RefreshLayout()
    {
        _messageText.ForceMeshUpdate();
        _messageText.rectTransform.sizeDelta = _messageText.GetPreferredValues();
        if ((_viewModel.log.propertyFlags & LogPropertyFlags.WithDateTime) == LogPropertyFlags.WithDateTime)
        {
            _dateTimeText.gameObject.SetActive(true);
            _dateTimeText.ForceMeshUpdate();
            _dateTimeText.rectTransform.sizeDelta = _dateTimeText.GetPreferredValues();
        }
        else
            _dateTimeText.gameObject.SetActive(false);
        if ((_viewModel.log.propertyFlags & LogPropertyFlags.WithLevel) == LogPropertyFlags.WithLevel && _viewModel.log.level != LogLevel.None)
        {
            _levelText.gameObject.SetActive(true);
            _levelText.ForceMeshUpdate();
            _levelText.rectTransform.sizeDelta = _levelText.GetPreferredValues();
        }
        else
            _levelText.gameObject.SetActive(false);
    }

    public void SetHighlight(HighlightType type)
    {
        _viewModel?.SetHighlight(type);
    }

    public Color GetHighlightColor(HighlightType type)
    {
        return type switch
        {
            HighlightType.New => _highlightColor_New,
            HighlightType.Search => _highlightColor_Search,
            HighlightType.Click => _highlightColor_Click,
            _ => Color.white
        };
    }

    public float GetHighlightDuration(HighlightType type)
    {
        return Mathf.Max(0.01f, type switch
        {
            HighlightType.New => _highlightDuration_New,
            HighlightType.Search => _highlightDuration_Search,
            HighlightType.Click => _highlightDuration_Click,
            _ => 0.01f
        });
    }
}