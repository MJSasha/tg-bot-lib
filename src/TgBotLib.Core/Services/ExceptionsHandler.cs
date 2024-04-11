using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotLib.Core.Services;

internal class ExceptionsHandler : IExceptionsHandler
{
    public Task Handle(Exception ex, ITelegramBotClient botClient, Update update)
    {
        return Task.CompletedTask;
        ;
    }
}