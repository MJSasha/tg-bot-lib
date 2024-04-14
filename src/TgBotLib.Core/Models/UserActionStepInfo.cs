namespace TgBotLib.Core.Models;

public class UserActionStepInfo
{
    public int Step { get; private set; }
    public string ActionName { get; private set; }
    private object _payload;

    public UserActionStepInfo(string actionName, object payload)
    {
        Step = 0;
        ActionName = actionName;
        _payload = payload;
    }

    public UserActionStepInfo IncrementStep()
    {
        Step++;
        return this;
    }

    public T GetPayload<T>()
    {
        return (T)_payload;
    }

    public void UpdatePayload(object payload)
    {
        _payload = payload;
    }
}