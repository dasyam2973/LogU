using System;
using UnityEngine;

public class UIFilterItem_DateTimeRange : UIFilterItem
{
    [SerializeField] private UICheckBox _useCheckBox;
    [SerializeField] private UIDateTimeField _fromTimeField;
    [SerializeField] private UIDateTimeField _toTimeField;

    private bool _useChecked;
    private DateTime _fromTime;
    private bool _fromValid;
    private DateTime _toTime;
    private bool _toValid;

    public override bool UseShouldInclude => _useChecked && _fromValid && _toValid;

    public override bool ShouldInclude(Log log)
    {
        if (!_useChecked || !_fromValid || !_toValid)
            return false;
        return _fromTime.TimeOfDay <= log.dateTime.TimeOfDay && log.dateTime.TimeOfDay <= _toTime.TimeOfDay;
    }

    public override bool ShouldExclude(Log log)
    {
        return false;
    }

    public override void Save()
    {
        _useChecked = _useCheckBox && _useCheckBox.Checked;
        if (_fromTimeField && _fromTimeField.TryGetValue(out DateTime time))
        {
            _fromTime = time;
            _fromValid = true;
        }
        else
            _fromValid = false;
        if (_toTimeField && _toTimeField.TryGetValue(out time))
        {
            _toTime = time;
            _toValid = true;
        }
        else
            _toValid = false;
    }
}