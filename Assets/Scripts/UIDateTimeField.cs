using System;
using System.Text;
using TMPro;
using UnityEngine;

public class UIDateTimeField : UIBehaviour
{
    [SerializeField] private TMP_InputField _hourInputField;
    [SerializeField] private TMP_InputField _minuteInputField;
    [SerializeField] private TMP_InputField _secondInputField;

    public bool TryGetValue(out DateTime result)
    {
        if (int.TryParse(_hourInputField.text, out int hour) && int.TryParse(_minuteInputField.text, out int minute) && int.TryParse(_secondInputField.text, out int second))
        {
            result = new(2025, 9, 25, hour, minute, second);
            return true;
        }
        result = default;
        return false;
    }

    public void SetValue(TimeSpan time)
    {
        _hourInputField.text = time.Hours.ToString();
        _minuteInputField.text = time.Minutes.ToString();
        _secondInputField.text = time.Seconds.ToString();
    }

    public void Clear()
    {
        _hourInputField.text = string.Empty;
        _minuteInputField.text = string.Empty;
        _secondInputField.text = string.Empty;
    }

    private void Awake()
    {
        _hourInputField.onValueChanged.AddListener((string s) => OnValueChanged(_hourInputField, 23));
        _hourInputField.onEndEdit.AddListener((string s) => OnEndEdit(_hourInputField));

        _minuteInputField.onValueChanged.AddListener((string s) => OnValueChanged(_minuteInputField, 59));
        _minuteInputField.onEndEdit.AddListener((string s) => OnEndEdit(_minuteInputField));

        _secondInputField.onValueChanged.AddListener((string s) => OnValueChanged(_secondInputField, 59));
        _secondInputField.onEndEdit.AddListener((string s) => OnEndEdit(_secondInputField));
    }

    private void OnValueChanged(TMP_InputField inputField, int max)
    {
        if (string.IsNullOrEmpty(inputField.text))
            return;

        StringBuilder sb = new();
        foreach (char c in inputField.text)
        {
            if (char.IsDigit(c))
                sb.Append(c);
        }
        if (sb.Length > 2)
            sb.Remove(0, sb.Length - 2);

        if (int.TryParse(sb.ToString(), out int value))
        {
            value = Mathf.Clamp(value, 0, max);
            inputField.text = value.ToString().PadLeft(2, '0');
            inputField.caretPosition = 2;
        }
        else
            inputField.text = string.Empty;
    }

    private void OnEndEdit(TMP_InputField inputField)
    {
        inputField.textComponent.rectTransform.anchoredPosition = Vector2.zero;
        var caret = inputField.transform.GetChild(0).Find("Caret");
        if (caret)
        {
            var caretRect = caret.GetComponent<RectTransform>();
            caretRect.anchoredPosition = Vector2.zero;
        }
    }
}