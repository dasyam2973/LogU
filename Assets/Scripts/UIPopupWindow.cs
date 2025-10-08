using UnityEngine;

public class UIPopupWindow : UIDraggableObject, IPopup
{
    [SerializeField] private UIBlocker _blocker;

    public bool Visible => gameObject.activeSelf;

    private void Awake()
    {
        if (_blocker)
        {
            _blocker.onClick.AddListener(() =>
            {
                if (gameObject.activeSelf)
                    Hide();
            });
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Open()
    {
        Show();
    }

    public virtual void Close()
    {
        Hide();
    }

    private void OnEnable()
    {
        if (_blocker)
            _blocker.gameObject.SetActive(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (_blocker)
            _blocker.gameObject.SetActive(false);
    }
}