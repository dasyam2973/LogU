public static class StringExtensions
{
    public static string ToRichTextString(this LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "<color=#999AA6>[Trace]</color>",
            LogLevel.Debug => "<color=#65DB31>[Debug]</color>",
            LogLevel.Warning => "<color=#FFB13D>[Warning]</color>",
            LogLevel.Error => "<color=#F3514A>[Error]</color>",
            _ => string.Empty
        };
    }
}