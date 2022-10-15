using System;

namespace TgBotLib
{
    public static class BaseBotSettings
    {
        internal static string BotToken { get => _botToken ?? throw new NotImplementedException("Bot can't start, because you haven't set token."); }
        internal static string BackRoot { get => _backRoot ?? throw new NotImplementedException("For this service to work, you need to set path to api."); }

        private static string _botToken;
        private static string _backRoot;

        public static void SetSettings(string botToken, string backRoot = null)
        {
            _botToken = botToken;
            _backRoot = backRoot;
        }
    }
}
