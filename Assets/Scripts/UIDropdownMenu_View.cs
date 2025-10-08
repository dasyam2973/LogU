using UnityEngine;

public class UIDropdownMenu_View : UIDropdownMenu
{
    [SerializeField] private UIPopupWindow _filterWindow;

    public void OnClick_Highlight()
    {
        Hide();
        MainController.Instance.GlobalMessageBox.Show("Alert", "Not implemented.");
    }

    public void OnClick_Filter()
    {
        Hide();
        _filterWindow.Open();
    }
}