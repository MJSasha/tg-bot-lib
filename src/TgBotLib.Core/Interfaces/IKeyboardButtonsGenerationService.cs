using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLib.Core;

public interface IKeyboardButtonsGenerationService
{
    IReplyMarkup GetButtons();
    void SetKeyboardButtons(params string[] markup);
    void SetKeyboardButtons(params string[][] markup);
}