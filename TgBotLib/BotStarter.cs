using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using TgBotLib.Services;

namespace TgBotLib
{
    public class BotStarter
    {
        private readonly TelegramBotClient client;
        private readonly EventHandler<MessageEventArgs> distributeMessages;
        private readonly EventHandler<CallbackQueryEventArgs> distributeCallbacks;

        public BotStarter(EventHandler<MessageEventArgs> distributeMessages, EventHandler<CallbackQueryEventArgs> distributeCallbacks)
        {
            client = SingletonService.GetClient();
            this.distributeMessages = distributeMessages;
            this.distributeCallbacks = distributeCallbacks;
        }

        public void Start(Action onAfterStart = null)
        {
            try
            {
                client.StartReceiving();
                LogService.LogStart();

                onAfterStart?.Invoke();
                client.OnMessage += distributeMessages;
                client.OnCallbackQuery += distributeCallbacks;

                while (true) Thread.Sleep(int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
