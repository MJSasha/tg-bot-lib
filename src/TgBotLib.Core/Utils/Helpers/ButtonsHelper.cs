using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLib.Core;

public static class ButtonsHelper
{
    public static IReplyMarkup CreateButtonWithContactRequest(string buttonText)
    {
        return new ReplyKeyboardMarkup(KeyboardButton.WithRequestContact(buttonText));
    }
}