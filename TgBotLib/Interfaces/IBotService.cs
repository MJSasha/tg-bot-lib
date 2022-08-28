using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLib.Interfaces
{
    public interface IBotService
    {
        Task DeleteMessage(int messageId);
        Task SendMessage(string message, IReplyMarkup buttons = null);
        Task EditMessage(string message, int messageId, IReplyMarkup buttons = null);
    }
}
