using System;
using TMPro;
using UnityEngine;

public class UIFilterItem_ExcludeString : UIFilterItem
{
    [SerializeField] private UICheckBox _useCheckBox;
    [SerializeField] private UICheckBox _ignoreCaseCheckBox;
    [SerializeField] private TMP_InputField _inputField;

    private bool _useChecked;
    private bool _ignoreCaseChecked;
    private string _keyword;

    public override bool ShouldInclude(Log log)
    {
        return false;
    }

    public override bool ShouldExclude(Log log)
    {
        if (!_useChecked)
            return false;
        return log.message.Contains(_keyword, _ignoreCaseChecked ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
    }

    public override void Save()
    {
        _useChecked = _useCheckBox && _useCheckBox.Checked;
        _ignoreCaseChecked = _ignoreCaseCheckBox && _ignoreCaseCheckBox.Checked;
        _keyword = _inputField.text;
    }
}