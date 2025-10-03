using TMPro;
using UnityEngine;

public class UIPopupWindow_MessageBox : UIPopupWindow
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _contentText;

    public void Show(string title, string content)
    {
        _titleText.text = title;
        _contentText.text = content;
        Show();
    }
}