using SFB;

public class UIDropdownMenu_File : UIDropdownMenu
{
    public void OnClick_OpenFile()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Select a file", "", new ExtensionFilter[] { new("", "txt", "log") }, false);
        if (paths.Length > 0)
        {
            if (MainController.Instance.LogManager)
                MainController.Instance.LogManager.Path = paths[0];
        }
        Hide();
    }
}