using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBotLib.Interfaces;

namespace TgBotLib.Handlers
{
    public abstract class BaseSpecialHandler
    {
        protected Task currentTask;
        protected CancellationTokenSource сancellationToken;
        protected readonly IBotService bot;

        protected BaseSpecialHandler(IBotService bot)
        {
            this.bot = bot;
        }

        public virtual async Task ProcessMessage(Message message)
        {
            if (сancellationToken == null) await Task.Run(() => RegistrateProcessing());
            if (!сancellationToken.IsCancellationRequested) currentTask.Start();
        }

        protected abstract void RegistrateProcessing();

        protected void AddProcessing(string message, Action action, Action completeAction = null, IReplyMarkup button = null)
        {
            сancellationToken = new();
            currentTask = new Task(() =>
            {
                action?.Invoke();
                сancellationToken.Cancel();
            });
            bot.SendMessage(message, button);
            currentTask.Wait();

            completeAction?.Invoke();
        }
    }
}
