using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    [SerializeField] private UILogItem _logItem;
    [SerializeField] private Transform _logContainer;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private UIPopupWindow_Filter _filterWindow;
    [SerializeField] private TextMeshProUGUI _logCountText;
    [SerializeField] private TextMeshProUGUI _filePathText;

    private float SingleLineHeight => _logItem.Height;
    private float _maxLineWidth;

    public bool IsProcessingEvent { get; private set; }
    private Queue<FileSystemEventArgs> _pendingEventQueue = new();

    private string _path;
    private FileSystemWatcher _watcher;
    private long _lastPosition;
    private Queue<string> _messageQueue = new();

    private List<UILogItem> _logItems = new();
    private List<LogViewModel> _allLogs = new();
    private List<LogViewModel> _showingLogs = new();

    private ILogFilter[] _filters;

    public string Path
    {
        set
        {
            _path = value;
            _lastPosition = 0;
            if (_watcher != null)
            {
                _watcher.Path = System.IO.Path.GetDirectoryName(value);
                _watcher.Filter = System.IO.Path.GetFileName(value);
            }
            if (!string.IsNullOrEmpty(_path) && File.Exists(_path))
                _filePathText.text = $"Current File: {_path}";
            else
                _filePathText.text = $"Current File: -";
        }
    }

    private void Awake()
    {
        _watcher = new()
        {
            NotifyFilter = NotifyFilters.LastWrite,
            EnableRaisingEvents = true
        };
        _watcher.Changed += OnChanged;
        _scrollRect.onValueChanged.AddListener((Vector2 vec2) =>
        {
            RefreshView();
        });
    }

    private void OnDestroy()
    {
        _watcher?.Dispose();
    }

    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject gameObject = Instantiate(_logItem.gameObject, _logContainer);
            gameObject.SetActive(false);
            _logItems.Add(gameObject.GetComponent<UILogItem>());
        }
    }

    private void Update()
    {
        lock (_pendingEventQueue)
        {
            if (!IsProcessingEvent && _pendingEventQueue.Count > 0)
                OnChanged(null, _pendingEventQueue.Dequeue());
        }
        lock (_messageQueue)
        {
            if (_messageQueue.Count > 0)
            {
                string[] messages = new string[_messageQueue.Count];
                for (int i = 0; i < messages.Length; i++)
                {
                    messages[i] = _messageQueue.Dequeue();
                }
                AppendLogs(messages);
            }
        }
        _logCountText.text = $"{_showingLogs.Count} of {_allLogs.Count} Logs";
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        if (e == null || e.ChangeType != WatcherChangeTypes.Changed)
            return;
        if (IsProcessingEvent)
        {
            lock (_pendingEventQueue)
                _pendingEventQueue.Enqueue(e);
            return;
        }

        IsProcessingEvent = true;
        OnChangedAsync().Forget();
    }

    private async UniTaskVoid OnChangedAsync()
    {
        if (string.IsNullOrEmpty(_path) || !File.Exists(_path))
        {
            IsProcessingEvent = false;
            return;
        }

        using FileStream fs = new(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        fs.Seek(_lastPosition, SeekOrigin.Begin);
        using StreamReader reader = new(fs, Encoding.UTF8);
        while (reader.Peek() > -1)
        {
            string line = await reader.ReadLineAsync();
            if (!string.IsNullOrEmpty(line))
            {
                lock (_messageQueue)
                    _messageQueue.Enqueue(line);
            }
            _lastPosition = fs.Length;
        }
        IsProcessingEvent = false;
    }

    public bool TryGetShowingLogViewModel(int index, out LogViewModel logViewModel)
    {
        if (index < 0 || index >= _showingLogs.Count)
        {
            logViewModel = null;
            return false;
        }
        logViewModel = _showingLogs[index];
        return true;
    }

    public bool TryGetShowingLog(int index, out Log log)
    {
        if (index < 0 || index >= _showingLogs.Count)
        {
            log = new();
            return false;
        }
        log = _showingLogs[index].log;
        return true;
    }

    private bool ShouldInclude(Log log)
    {
        if (_filters == null || _filters.Length == 0)
            return true;

        bool useShouldInclude = false, shouldInclude = false;
        foreach (var filter in _filters)
        {
            if (filter.ShouldExclude(log))
                return false;
            if (filter.UseShouldInclude)
            {
                useShouldInclude = true;
                if (filter.ShouldInclude(log))
                    shouldInclude = true;
            }
        }
        return !useShouldInclude || shouldInclude;
    }

    public void ApplyFilters(params ILogFilter[] filters)
    {
        _filters = filters;
        _showingLogs.Clear();
        foreach (LogViewModel logViewModel in _allLogs)
        {
            if (ShouldInclude(logViewModel.log))
                _showingLogs.Add(logViewModel);
        }
        _maxLineWidth = 0f;
        RefreshView();
    }

    public async UniTaskVoid ScrollToCell(int index)
    {
        if (index < 0 || index >= _showingLogs.Count)
            return;

        await UniTask.NextFrame();

        float contentHeight = _scrollRect.content.rect.height;
        float viewportHeight = _scrollRect.viewport.rect.height;

        float normalizedY = 1f - (SingleLineHeight * index / (contentHeight - viewportHeight));
        normalizedY = Mathf.Clamp01(normalizedY);
        _scrollRect.verticalNormalizedPosition = normalizedY;
        _scrollRect.horizontalNormalizedPosition = 0f;
    }

    private void AppendLogWithoutRefreshView(Log log)
    {
        LogViewModel model = new(log, Time.time);
        _allLogs.Add(model);
        if (ShouldInclude(log))
        {
            _showingLogs.Add(model);
            model.SetHighlight(UILogItem.HighlightType.New);
            if (_scrollRect.verticalNormalizedPosition <= 0.001f)
                ScrollToCell(_showingLogs.Count - 1).Forget();
        }
    }

    public void AppendLogs(params Log[] logs)
    {
        foreach (Log log in logs)
        {
            AppendLogWithoutRefreshView(log);
        }
        RefreshView();
    }

    public void AppendLogs(params string[] messages)
    {
        foreach (string message in messages)
        {
            AppendLogWithoutRefreshView(new Log(message));
        }
        RefreshView();
    }

    public void AppendLog(Log log)
    {
        AppendLogWithoutRefreshView(log);
        RefreshView();
    }

    public void AppendLog(string message)
    {
        AppendLog(new Log(message));
    }

    public void RefreshView()
    {
        float contentY = _scrollRect.content.anchoredPosition.y;
        float viewportHeight = _scrollRect.viewport.rect.height;
        float viewportTop = -contentY;
        float viewportBottom = -contentY - viewportHeight;
        foreach (var logItem in _logItems)
        {
            logItem.gameObject.SetActive(false);
        }

        int startIndex = Mathf.Max(0, Mathf.FloorToInt(viewportTop / -SingleLineHeight));
        for (int i = startIndex, j = 0; i < _showingLogs.Count; i++)
        {
            float top = -SingleLineHeight * i;
            float bottom = top - SingleLineHeight;
            if (bottom <= viewportTop && top >= viewportBottom)
            {
                _logItems[j].RectTransform.SetAnchoredY(top);
                _logItems[j].SetViewModel(_showingLogs[i]);
                _logItems[j].gameObject.SetActive(true);
                j++;
            }
            else
                break;
        }

        _maxLineWidth = Mathf.Max(_maxLineWidth, _logItems.Max(logItem => logItem.Width));
        _scrollRect.content.sizeDelta = new(_maxLineWidth, SingleLineHeight * _showingLogs.Count);
    }

    public void ClearLogs()
    {
        _showingLogs.Clear();
        _maxLineWidth = 0f;
        _allLogs.Clear();
        RefreshView();
    }
}