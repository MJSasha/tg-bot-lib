using Telegram.Bot.Types;

namespace TgBotLib.Core;

public static class UpdateExtensions
{
    public static string GetMessageText(this Update update) => update?.Message?.Text ?? "";

    public static long GetChatId(this Update update) => update?.Message?.Chat.Id ?? 0;
}