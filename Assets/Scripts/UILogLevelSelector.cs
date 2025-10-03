using TMPro;
using UnityEngine;

public class UILogLevelSelector : UIBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemText;

    [SerializeField] private LogLevel[] _items;
    private int _index = 0;

    public bool TryGetSelectedItem(out LogLevel logLevel)
    {
        if (_items == null || _index < 0 || _index >= _items.Length)
        {
            logLevel = LogLevel.None;
            return false;
        }
        logLevel = _items[_index];
        return true;
    }

    public void RefreshView()
    {
        if (TryGetSelectedItem(out LogLevel logLevel) && logLevel != LogLevel.None)
            _itemText.text = logLevel.ToRichTextString();
        else
            _itemText.text = "None";
    }

    private void Start()
    {
        RefreshView();
    }

    public void ToLeftItem()
    {
        _index = Mathf.Max(0, _index - 1);
        RefreshView();
    }

    public void ToRightItem()
    {
        _index = Mathf.Min(_items == null ? 0 : _items.Length - 1, _index + 1);
        RefreshView();
    }
}