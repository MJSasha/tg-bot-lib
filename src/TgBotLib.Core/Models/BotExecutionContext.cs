using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotLib.Core.Models;

public class BotExecutionContext
{
    public ITelegramBotClient BotClient { get; private set; }
    public Update Update { get; private set; }

    internal BotExecutionContext SetBotClient(ITelegramBotClient botClient)
    {
        BotClient = botClient;
        return this;
    }

    internal BotExecutionContext SetUpdate(Update update)
    {
        Update = update;
        return this;
    }
}