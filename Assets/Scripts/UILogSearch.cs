using System;
using TMPro;
using UnityEngine;

public class UILogSearch : UIBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private UICheckBox _ignoreCaseCheckBox;

    [Header("Search Failed Message")]
    [SerializeField] private TextMeshProUGUI _searchFailedText;
    [SerializeField] private float _textDuration;
    private float _duration;

    private int _nextSearchIndex = 0;

    private void Awake()
    {
        _inputField.onEndEdit.AddListener(OnEndEdit);
    }

    public void SearchFailed(bool showMessage)
    {
        _nextSearchIndex = 0;
        if (showMessage)
            _duration = _textDuration;
    }

    private void Update()
    {
        _duration = Mathf.Max(0f, _duration - Time.deltaTime);
        _searchFailedText.color = _searchFailedText.color.WithAlpha(1f - Mathf.Pow(1f - _duration / _textDuration, 4f));
    }

    private void OnEndEdit(string s)
    {
        _nextSearchIndex = 0;
    }

    public void OnClick_Search()
    {
        if (string.IsNullOrEmpty(_inputField.text))
            SearchFailed(true);
        else
        {
            while (true)
            {
                if (MainController.Instance.LogManager.TryGetShowingLog(_nextSearchIndex, out Log log))
                {
                    if (log.message.Contains(_inputField.text, _ignoreCaseCheckBox.Checked ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                    {
                        if (MainController.Instance.LogManager.TryGetShowingLogViewModel(_nextSearchIndex, out LogViewModel logViewModel))
                            logViewModel.SetHighlight(UILogItem.HighlightType.Search);
                        MainController.Instance.LogManager.ScrollToCell(_nextSearchIndex++).Forget();
                        break;
                    }
                    _nextSearchIndex++;
                }
                else
                {
                    SearchFailed(true);
                    break;
                }
            }
        }
    }
}