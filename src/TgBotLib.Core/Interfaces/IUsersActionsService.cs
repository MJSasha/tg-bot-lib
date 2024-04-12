using TgBotLib.Core.Models;

namespace TgBotLib.Core;

public interface IUsersActionsService
{
    void HandleUser(long chatId, string actionName, object model);
    UserActionStepInfo? GetUserActionStepInfo(long chatId);
    void IncrementStep(long chatId);
    void RemoveUser(long chatId);
}