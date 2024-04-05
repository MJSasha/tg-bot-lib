using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLib.Core;

public interface IButtonsGenerationService
{
    IReplyMarkup GetButtons();
    void SetInlineButtons(params string[] markup);
    void SetInlineButtons(params string[][] markup);
    void SetInlineButtons(params (string name, string callback)[] markup);
    void SetInlineButtons(params (string name, string callback)[][] markup);
    void SetInlineUrlButtons(params (string name, string url)[] markup);
    void SetInlineUrlButtons(params (string name, string url)[][] markup);
}