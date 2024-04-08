namespace TgBotLib.Core;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class MessageAttribute : BaseAttribute
{
    public MessageAttribute(string message, bool isPattern = false, bool ignoreCase = false) : base(message, isPattern, ignoreCase)
    {
    }
}