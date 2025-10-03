public class LogViewModel
{
    public Log log;
    public UILogItem.HighlightType highlightType;
    public float highlightElapsed;

    private float _lastUpdatedTime;

    public LogViewModel(Log log, float time)
    {
        this.log = log;
        _lastUpdatedTime = time;
    }

    public void Update(float time)
    {
        float deltaTime = time - _lastUpdatedTime;
        highlightElapsed += deltaTime;
        _lastUpdatedTime = time;
    }

    public void SetHighlight(UILogItem.HighlightType type)
    {
        highlightType = type;
        highlightElapsed = 0f;
    }
}