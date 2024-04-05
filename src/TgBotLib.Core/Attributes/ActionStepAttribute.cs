namespace TgBotLib.Core;

[AttributeUsage(AttributeTargets.Method)]
public class ActionStepAttribute : Attribute
{
    public string ActionName { get; set; }
    public int Step { get; set; }

    public ActionStepAttribute(string actionName, int step)
    {
        ActionName = actionName;
        Step = step;
    }
}