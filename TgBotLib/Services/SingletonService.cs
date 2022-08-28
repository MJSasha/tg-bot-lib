using Telegram.Bot;

namespace TgBotLib.Services
{
    public static class SingletonService
    {
        private static TelegramBotClient TelegramClient { get; set; }

        public static TelegramBotClient GetClient()
        {
            if (TelegramClient == null)
            {
                TelegramClient = new TelegramBotClient(BaseBotSettings.BotToken);
                TelegramClient.OnMessage += LogService.LogMessages;
                TelegramClient.OnCallbackQuery += LogService.LogCallbacks;
            }
            return TelegramClient;
        }
    }
}
