namespace TgBotLib.Core;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CallbackAttribute : BaseAttribute
{
    public CallbackAttribute(string message, bool isPattern = false, bool ignoreCase = false) : base(message, isPattern, ignoreCase)
    {
    }
}