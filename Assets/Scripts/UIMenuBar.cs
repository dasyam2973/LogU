using UnityEngine;

public class UIMenuBar : UIBehaviour
{
    [SerializeField] private UIDropdownMenu _fileDropdownMenu;
    [SerializeField] private UIDropdownMenu _viewDropdownMenu;
    [SerializeField] private UIDropdownMenu _resolutionDropdownMenu;

    public void OnClick_File()
    {
        if (_fileDropdownMenu)
            _fileDropdownMenu.Show();
    }

    public void OnClick_View()
    {
        if (_viewDropdownMenu)
            _viewDropdownMenu.Show();
    }

    public void OnClick_Resolution()
    {
        if (_resolutionDropdownMenu)
            _resolutionDropdownMenu.Show();
    }

    public void OnClick_Help()
    {
        MainController.Instance.GlobalMessageBox.Show("Alert", "Not implemented.");
    }
}