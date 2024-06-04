using Telegram.Bot.Types.Enums;

namespace TgBotLib.Core;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class MessageAttribute : BaseAttribute
{
    public MessageType MessageType { get; set; }
    public MessageAttribute(string message, MessageType messageType, bool isPattern = false, bool ignoreCase = false) : base(message, isPattern, ignoreCase)
    {
        MessageType = messageType;
    }
}