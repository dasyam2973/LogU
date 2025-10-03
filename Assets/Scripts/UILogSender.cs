using System;
using TMPro;
using UnityEngine;

public class UILogSender : UIBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private UILogLevelSelector _logLevelSelector;

    public void OnClick_Send()
    {
        if (!string.IsNullOrEmpty(_inputField.text))
        {
            string[] messages = _inputField.text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (_logLevelSelector.TryGetSelectedItem(out LogLevel logLevel))
            {
                Log[] logs = new Log[messages.Length];
                for (int i = 0; i < logs.Length; i++)
                {
                    logs[i] = new(messages[i], logLevel);
                }
                MainController.Instance.LogManager.AppendLogs(logs);
            }
            else
                MainController.Instance.LogManager.AppendLogs(messages);
        }
        _inputField.text = string.Empty;
    }
}