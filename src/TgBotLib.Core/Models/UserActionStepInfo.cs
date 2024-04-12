namespace TgBotLib.Core.Models;

public class UserActionStepInfo
{
    public int Step { get; private set; }
    public string ActionName { get; private set; }
    private object Model { get; set; }

    public UserActionStepInfo(string actionName, object model)
    {
        Step = 0;
        ActionName = actionName;
        Model = model;
    }

    public UserActionStepInfo IncrementStep()
    {
        Step++;
        return this;
    }

    public T GetPayload<T>()
    {
        return (T)Model;
    }
}