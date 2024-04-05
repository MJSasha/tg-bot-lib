namespace TgBotLib.Core.Models;

public class UserActionStepInfo
{
    public int Step { get; private set; }
    public string ActionName { get; private set; }

    public UserActionStepInfo(string actionName)
    {
        Step = 0;
        ActionName = actionName;
    }

    public UserActionStepInfo IncrementStep()
    {
        Step++;
        return this;
    }
}