using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLib.Core;

public static class ClientExtensions
{
    public static char[] ChartsForEscape = ['[', ']', '(', ')', '~', '>', '<', '&', '#', '+', '-', '=', '|', '{', '}', '.', '!'];

    public static Task SendMdTextMessage(this ITelegramBotClient botClient,
        ChatId chatId,
        string text,
        int? messageThreadId = default,
        IEnumerable<MessageEntity>? entities = default,
        bool? disableWebPagePreview = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default)
    {
        return botClient.SendTextMessageAsync(chatId,
            text.EscapeMarkdownSpecialCharacters(),
            parseMode: ParseMode.MarkdownV2,
            messageThreadId: messageThreadId,
            entities: entities,
            disableWebPagePreview: disableWebPagePreview,
            disableNotification: disableNotification,
            protectContent: protectContent,
            replyToMessageId: replyToMessageId,
            allowSendingWithoutReply: allowSendingWithoutReply,
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken
        );
    }

    public static string EscapeMarkdownSpecialCharacters(this string input)
    {
        foreach (var specialChar in ChartsForEscape)
        {
            input = input.Replace(specialChar.ToString(), "\\" + specialChar);
        }

        return input;
    }
}