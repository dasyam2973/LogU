using UnityEngine.UI;
using UnityEngine;

public class UIDropdownMenu : UIBehaviour, IPopup
{
    [SerializeField] private UIBlocker _blocker;

    public bool Visible => gameObject.activeSelf;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

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

    private void OnEnable()
    {
        if (_blocker)
            _blocker.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (_blocker)
            _blocker.gameObject.SetActive(false);
    }
}