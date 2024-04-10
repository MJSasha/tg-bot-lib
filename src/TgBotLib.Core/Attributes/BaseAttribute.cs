namespace TgBotLib.Core;

public abstract class BaseAttribute : Attribute
{
    public string Message { get; set; }
    public bool IsPattern { get; set; }
    public bool IgnoreCase { get; set; }

    protected BaseAttribute(string message, bool isPattern = false, bool ignoreCase = false)
    {
        IsPattern = isPattern;
        IgnoreCase = ignoreCase;
        Message = !isPattern && ignoreCase ? message.ToLower() : message;
    }
}