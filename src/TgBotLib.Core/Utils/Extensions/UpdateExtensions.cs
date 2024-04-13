using Telegram.Bot.Types;

namespace TgBotLib.Core;

public static class UpdateExtensions
{
    public static string GetMessageText(this Update update) => update?.Message?.Text ?? "";

    public static string GetCallbackMessage(this Update update) => update?.CallbackQuery?.Data ?? "";

    public static long GetCallbackChatId(this Update update) => update?.CallbackQuery?.Message?.Chat?.Id ?? 0;

    public static long GetChatId(this Update update) => update?.Message?.Chat.Id
                                                        ?? update?.CallbackQuery?.Message?.Chat?.Id
                                                        ?? 0;

    public static string GetInputText(this Update update) => update?.Message?.Text
                                                             ?? update?.CallbackQuery?.Data
                                                             ?? "";
}