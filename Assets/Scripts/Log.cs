using System;

public struct Log
{
    public string message;
    public DateTime dateTime;
    public LogPropertyFlags propertyFlags;
    public LogLevel level;

    public Log(string message, DateTime dateTime, LogPropertyFlags propertyFlags, LogLevel level)
    {
        this.message = message;
        this.dateTime = dateTime;
        this.propertyFlags = propertyFlags;
        this.level = level;
    }
    public Log(string message) : this(message, DateTime.Now, LogPropertyFlags.WithDateTime, LogLevel.None) { }
    public Log(string message, DateTime dateTime) : this(message, dateTime, LogPropertyFlags.WithDateTime, LogLevel.None) { }
    public Log(string message, LogPropertyFlags propertyFlags) : this(message, DateTime.Now, propertyFlags, LogLevel.None) { }
    public Log(string message, LogLevel level) : this(message, DateTime.Now, LogPropertyFlags.WithDateTime | LogPropertyFlags.WithLevel, level) { }
}