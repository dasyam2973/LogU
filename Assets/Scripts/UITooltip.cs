public class UITooltip : UIBehaviour
{
    public bool Visible
    {
        get => gameObject.activeSelf;
        set
        {
            gameObject.SetActive(value);
        }
    }

    public void Show()
    {
        Visible = true;
    }

    public void Hide()
    {
        Visible = false;
    }
}