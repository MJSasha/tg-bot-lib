using Telegram.Bot;
using Telegram.Bot.Types;
using TgBotLib.Core.Models;

namespace TgBotLib.Core.Base;

public abstract class BotController
{
    public BotExecutionContext BotContext { get; set; }
    protected ITelegramBotClient Client => BotContext.BotClient;
    protected Update Update => BotContext.Update;
}