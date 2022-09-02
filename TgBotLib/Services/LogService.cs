using System;
using Telegram.Bot.Args;

namespace TgBotLib.Services
{
    public static class LogService
    {
        public static void LogInfo(string info) => BaseLog(LogType.INFO, info);
        public static void LogWarn(string warn) => BaseLog(LogType.WARN, warn);
        public static void LogError(string error) => BaseLog(LogType.ERROR, error);

        public static void LogServerNotFound(string actionName)
        {
            LogError($"Server not found (404). {actionName} - Not completed");
        }

        internal static void LogStart()
        {
            Console.Write($"{DateTime.Now} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("BOT START");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("-----------------------------------------------------" +
                $"\nToken: {BaseBotSettings.BotToken}");

            try { Console.WriteLine($"BackEnd Root: {BaseBotSettings.BackRoot}"); } catch { /*don't send root*/}

            Console.WriteLine("-----------------------------------------------------\n");
        }

        internal static void LogMessages(object sender, MessageEventArgs e)
        {
            LogInfo($"ChatId: {e.Message.Chat.Id} | Message: {e.Message.Text}");
        }

        internal static void LogCallbacks(object sender, CallbackQueryEventArgs e)
        {
            LogInfo($"ChatId: {e.CallbackQuery.Message.Chat.Id} | Callback: {e.CallbackQuery.Data}");
        }

        private static void BaseLog(LogType logType, string message)
        {
            Console.Write(DateTime.Now);
            Console.ForegroundColor = logType.GetCollor();
            Console.Write($" {logType}");
            Console.ResetColor();
            Console.WriteLine($" --- {message}");
        }

        private enum LogType
        {
            INFO,
            WARN,
            ERROR
        }

        private static ConsoleColor GetCollor(this LogType logType)
        {
            return logType switch
            {
                LogType.INFO => ConsoleColor.Green,
                LogType.WARN => ConsoleColor.DarkYellow,
                LogType.ERROR => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
        }
    }
}
