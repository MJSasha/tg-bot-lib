using TgBotLib.Core.Models;

namespace TgBotLib.Core.Base;

public abstract class BotController
{
    public BotExecutionContext BotContext { get; set; }
}