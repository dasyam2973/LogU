public interface ILogFilter
{
    public bool UseShouldInclude { get; }
    public bool ShouldInclude(Log log);
    public bool ShouldExclude(Log log);
}