using UnityEngine;

public class MainController : SingletonBehaviour<MainController>
{
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Canvas _popupCanvas;
    [SerializeField] private LogManager _logManager;
    [SerializeField] private UIPopupWindow_MessageBox _globalMessageBox;

    public Canvas MainCanvas => _mainCanvas;
    public Canvas PopupCanvas => _popupCanvas;
    public LogManager LogManager => _logManager;
    public UIPopupWindow_MessageBox GlobalMessageBox => _globalMessageBox;

    protected override void Awake()
    {
        base.Awake();
        Screen.SetResolution(1280, 720, false);
    }
}