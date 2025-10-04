using UnityEngine;

public class UIFilterItem_Level : UIFilterItem
{
    [SerializeField] private UICheckBox _useCheckBox;
    [SerializeField] private UICheckBox _includeTraceCheckBox;
    [SerializeField] private UICheckBox _includeDebugCheckBox;
    [SerializeField] private UICheckBox _includeWarningCheckBox;
    [SerializeField] private UICheckBox _includeErrorCheckBox;

    private bool _useChecked;
    private bool _includeTraceChecked;
    private bool _includeDebugChecked;
    private bool _includeWarningChecked;
    private bool _includeErrorChecked;

    public override bool UseShouldInclude => _useChecked;

    public override bool ShouldInclude(Log log)
    {
        return log.level switch
        {
            LogLevel.Trace => _includeTraceChecked,
            LogLevel.Debug => _includeDebugChecked,
            LogLevel.Warning => _includeWarningChecked,
            LogLevel.Error => _includeErrorChecked,
            _ => true
        };
    }

    public override bool ShouldExclude(Log log)
    {
        return false;
    }

    public override void Save()
    {
        _useChecked = _useCheckBox && _useCheckBox.Checked;
        _includeTraceChecked = _includeTraceCheckBox && _includeTraceCheckBox.Checked;
        _includeDebugChecked = _includeDebugCheckBox && _includeDebugCheckBox.Checked;
        _includeWarningChecked = _includeWarningCheckBox && _includeWarningCheckBox.Checked;
        _includeErrorChecked = _includeErrorCheckBox && _includeErrorCheckBox.Checked;
    }
}