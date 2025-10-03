using System;

[Flags]
public enum LogPropertyFlags
{
    None = 0,
    WithDateTime = 0b1,
    WithLevel = 0b10
}