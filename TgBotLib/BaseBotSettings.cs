namespace TgBotLib
{
    public static class BaseBotSettings
    {
        public static string BotToken { get; set; }
        public static string ApiToken { get; set; }
        public static string BackRoot { get; set; }

        public static void SetSettings(string botToken, string apiToken, string backRoot)
        {
            BotToken = botToken;
            ApiToken = apiToken;
            BackRoot = backRoot;
        }
    }
}
