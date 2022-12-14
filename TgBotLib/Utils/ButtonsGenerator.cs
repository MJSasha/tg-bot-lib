using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLib.Utils
{
    public class ButtonsGenerator
    {
        private readonly List<List<InlineKeyboardButton>> returnsButtons = new();

        public static IReplyMarkup GetKeyboardButtonWithPhoneRequest(string buttonText) => new ReplyKeyboardMarkup(KeyboardButton.WithRequestContact(buttonText));

        public IReplyMarkup GetButtons()
        {
            return new InlineKeyboardMarkup(returnsButtons);
        }

        public void SetInlineButtons(params string[] markup) => SetInlineButtons(new[] { markup });
        public void SetInlineButtons(params string[][] markup) => AddButtons(markup, (lineMarkup) => lineMarkup.Select(text => InlineKeyboardButton.WithCallbackData(text, "@" + text)).ToList());

        public void SetInlineButtons(params (string name, string callback)[] markup) => SetInlineButtons(new[] { markup });
        public void SetInlineButtons(params (string name, string callback)[][] markup) => AddButtons(markup, (lineMarkup) => lineMarkup.Select(b => InlineKeyboardButton.WithCallbackData(b.name, "@" + b.callback)).ToList());

        public void SetInlineUrlButtons(params (string name, string url)[] markup) => SetInlineUrlButtons(new[] { markup });
        public void SetInlineUrlButtons(params (string name, string url)[][] markup) => AddButtons(markup, (lineMarkup) => lineMarkup.Select(b => InlineKeyboardButton.WithUrl(b.name, b.url)).ToList());

        private void AddButtons<T>(T[][] markup, Func<T[], List<InlineKeyboardButton>> createLine)
        {
            foreach (var lineMarkup in markup)
            {
                returnsButtons.Add(createLine(lineMarkup));
            }
        }
    }
}
