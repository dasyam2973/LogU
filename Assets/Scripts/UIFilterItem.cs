public abstract class UIFilterItem : UIBehaviour, ILogFilter
{
    public virtual bool UseShouldInclude => false;
    public abstract bool ShouldInclude(Log log);
    public abstract bool ShouldExclude(Log log);
    public abstract void Save();
}