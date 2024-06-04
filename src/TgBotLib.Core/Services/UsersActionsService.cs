using TgBotLib.Core.Models;

namespace TgBotLib.Core.Services;

internal class UsersActionsService : IUsersActionsService
{
    private readonly Dictionary<long, UserActionStepInfo?> _userActionStepInfos = [];

    public void HandleUser(long chatId, string actionName, object payload = null)
    {
        lock (_userActionStepInfos)
        {
            _userActionStepInfos.Add(chatId, new UserActionStepInfo(actionName, payload));
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

    public void DecrementStep(long chatId)
    {
        lock (_userActionStepInfos)
        {
            var info = _userActionStepInfos.GetValueOrDefault(chatId);
            info.DecrementStep();
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