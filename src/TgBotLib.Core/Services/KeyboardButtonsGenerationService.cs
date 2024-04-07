using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLib.Core.Services;

internal class KeyboardButtonsGenerationService : IKeyboardButtonsGenerationService
{
    private readonly List<List<KeyboardButton>> _returnsButtons = [];

    public IReplyMarkup GetButtons()
    {
        return new ReplyKeyboardMarkup(_returnsButtons);
    }

    public void SetKeyboardButtons(params string[] markup) => SetKeyboardButtons([markup]);
    public void SetKeyboardButtons(params string[][] markup) => AddButtons(markup, (lineMarkup) => lineMarkup.Select(text => new KeyboardButton(text)).ToList());

    private void AddButtons<T>(IEnumerable<T[]> markup, Func<T[], List<KeyboardButton>> createLine)
    {
        foreach (var lineMarkup in markup)
        {
            _returnsButtons.Add(createLine(lineMarkup));
        }
    }
}