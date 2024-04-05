using TgBotLib.Core.Models;

namespace TgBotLib.Core.Services;

internal class UsersActionsService : IUsersActionsService
{
    private readonly Dictionary<long, UserActionStepInfo?> _userActionStepInfos = [];

    public void HandleUser(long chatId, string actionName)
    {
        lock (_userActionStepInfos)
        {
            _userActionStepInfos.Add(chatId, new UserActionStepInfo(actionName));
        }
    }

    public UserActionStepInfo? GetUserActionStepInfo(long chatId)
    {
        lock (_userActionStepInfos)
        {
            return _userActionStepInfos.GetValueOrDefault(chatId);
        }
    }

    public void IncrementStep(long chatId)
    {
        lock (_userActionStepInfos)
        {
            var info = _userActionStepInfos.GetValueOrDefault(chatId);
            info.IncrementStep();
        }
    }

    public void RemoveUser(long chatId)
    {
        lock (_userActionStepInfos)
        {
            _userActionStepInfos.Remove(chatId);
        }
    }
}