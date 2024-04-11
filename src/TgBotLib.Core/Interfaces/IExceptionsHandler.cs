using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotLib.Core;

public interface IExceptionsHandler
{
    Task Handle(Exception ex, ITelegramBotClient botClient, Update update);
}