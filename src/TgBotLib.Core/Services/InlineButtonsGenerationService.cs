using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLib.Core.Services;

internal class InlineButtonsGenerationService : IInlineButtonsGenerationService
{
    private readonly List<List<InlineKeyboardButton>> _returnsButtons = [];

    public IReplyMarkup GetButtons()
    {
        return new InlineKeyboardMarkup(_returnsButtons);
    }

    public void SetInlineButtons(params string[] markup) => SetInlineButtons([markup]);

    public void SetInlineButtons(params string[][] markup)
    {
        foreach (var buttons in markup)
        {
            foreach (var text in buttons) ValidationHelper.ValidateCallback(text);
        }

        AddButtons(markup, (lineMarkup) => lineMarkup.Select(text => InlineKeyboardButton.WithCallbackData(text, text)).ToList());
    }

    public void SetInlineButtons(params (string name, string callback)[] markup) => SetInlineButtons([markup]);

    public void SetInlineButtons(params (string name, string callback)[][] markup)
    {
        foreach (var buttons in markup)
        {
            foreach (var button in buttons) ValidationHelper.ValidateCallback(button.callback);
        }

        AddButtons(markup, (lineMarkup) => lineMarkup.Select(b => InlineKeyboardButton.WithCallbackData(b.name, b.callback)).ToList());
    }

    public void SetInlineUrlButtons(params (string name, string url)[] markup) => SetInlineUrlButtons([markup]);
    public void SetInlineUrlButtons(params (string name, string url)[][] markup) => AddButtons(markup, (lineMarkup) => lineMarkup.Select(b => InlineKeyboardButton.WithUrl(b.name, b.url)).ToList());

    private void AddButtons<T>(IEnumerable<T[]> markup, Func<T[], List<InlineKeyboardButton>> createLine)
    {
        foreach (var lineMarkup in markup)
        {
            _returnsButtons.Add(createLine(lineMarkup));
        }
    }
}