using UnityEngine;

public class UIDropdownMenu_Resolution : UIDropdownMenu
{
    public void OnClick_640()
    {
        Screen.SetResolution(640, 360, false);
        Hide();
    }

    public void OnClick_1280()
    {
        Screen.SetResolution(1280, 720, false);
        Hide();
    }

    public void OnClick_1920()
    {
        Screen.SetResolution(1920, 1080, false);
        Hide();
    }
}