using System;
using UnityEngine;

public class UIFilterItem_DateTimeRange : UIFilterItem
{
    [SerializeField] private UICheckBox _useCheckBox;
    [SerializeField] private UIDateTimeField _fromTimeField;
    [SerializeField] private UIDateTimeField _toTimeField;

    private bool _useChecked;
    private DateTime? _fromTime;
    private DateTime? _toTime;

    public override bool UseShouldInclude => _useChecked && _fromTime != null && _toTime != null;

    public override bool ShouldInclude(Log log)
    {
        if (_fromTime == null || _toTime == null)
            return false;
        return _fromTime.Value.TimeOfDay <= log.dateTime.TimeOfDay && log.dateTime.TimeOfDay <= _toTime.Value.TimeOfDay;
    }

    public override bool ShouldExclude(Log log)
    {
        return false;
    }

    public override void SaveSetting()
    {
        _useChecked = _useCheckBox && _useCheckBox.Checked;
        if (_fromTimeField && _fromTimeField.TryGetValue(out DateTime time))
            _fromTime = time;
        else
            _fromTime = null;
        if (_toTimeField && _toTimeField.TryGetValue(out time))
            _toTime = time;
        else
            _toTime = null;
    }

    public override void ResetSetting()
    {
        _useChecked = false;
        _fromTime = null;
        _toTime = null;
    }

    public override void RefreshView()
    {
        if (_useCheckBox)
            _useCheckBox.Checked = _useChecked;
        if (_fromTimeField)
        {
            if (_fromTime == null)
                _fromTimeField.Clear();
            else
                _fromTimeField.SetValue(_fromTime.Value.TimeOfDay);
        }
        if (_toTimeField)
        {
            if (_toTime == null)
                _toTimeField.Clear();
            else
                _toTimeField.SetValue(_toTime.Value.TimeOfDay);
        }
    }
}