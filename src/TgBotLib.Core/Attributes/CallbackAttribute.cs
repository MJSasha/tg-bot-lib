namespace TgBotLib.Core;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CallbackAttribute : BaseAttribute
{
    public CallbackAttribute(string message, bool isPattern = false) : base(message, isPattern)
    {
    }
}