namespace TgBotLib.Core;

public abstract class BaseAttribute : Attribute
{
    public string Message { get; set; }
    public bool IsPattern { get; set; }

    protected BaseAttribute(string message, bool isPattern = false)
    {
        Message = message;
        IsPattern = isPattern;
    }
}