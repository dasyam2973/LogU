using System;
using UnityEngine;

public class UIPopupWindow_Filter : UIPopupWindow
{
    [SerializeField] private UIFilterItem[] _filterItems;

    public void SaveFilterSettings()
    {
        Array.ForEach(_filterItems, item => item.SaveSetting());
    }

    public void ResetFilterSettings()
    {
        Array.ForEach(_filterItems, item =>
        {
            item.ResetSetting();
            item.RefreshView();
        });
    }

    public void RefreshView()
    {
        Array.ForEach(_filterItems, item => item.RefreshView());
    }

    public void ApplyFilters()
    {
        SaveFilterSettings();
        MainController.Instance.LogManager.ApplyFilters(_filterItems);
    }

    public void OnClick_CopyFilterSettings()
    {
        MainController.Instance.GlobalMessageBox.Show("Alert", "Not implemented.");
    }

    public void OnClick_PasteFilterSettings()
    {
        MainController.Instance.GlobalMessageBox.Show("Alert", "Not implemented.");
    }

    public override void Open()
    {
        base.Open();
        RefreshView();
    }

    public override void Close()
    {
        base.Close();
        ApplyFilters();
    }
}