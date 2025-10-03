using UnityEngine;

public class UIPopupWindow_Filter : UIPopupWindow
{
    [SerializeField] private UIFilterItem[] _filterItems;

    public void SaveFiltersSetting()
    {
        foreach (var filterItem in _filterItems)
        {
            filterItem.Save();
        }
    }

    public void ApplyFilters()
    {
        SaveFiltersSetting();
        MainController.Instance.LogManager.ApplyFilters(_filterItems);
    }

    public override void Close()
    {
        base.Close();
        ApplyFilters();
    }
}