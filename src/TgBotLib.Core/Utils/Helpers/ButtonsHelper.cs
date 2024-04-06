using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLib.Core;

public static class ButtonsHelper
{
    public static IReplyMarkup CreateButtonWithContactRequest(string buttonText) => new ReplyKeyboardMarkup(KeyboardButton.WithRequestContact(buttonText));
}