namespace TgBotLib.Core;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class MessageAttribute : Attribute
{
    public string Message { get; set; }
    public bool IsPattern { get; set; }

    public MessageAttribute(string message, bool isPattern = false)
    {
        Message = message;
        IsPattern = isPattern;
    }
}