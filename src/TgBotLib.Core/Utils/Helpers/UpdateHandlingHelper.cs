using System.Reflection;
using TgBotLib.Core.Base;
using TgBotLib.Core.Models;

namespace TgBotLib.Core;

internal static class UpdateHandlingHelper
{
    internal static async Task HandleMessage(IEnumerable<BotController> controllers, string messageText)
    {
        if (string.IsNullOrEmpty(messageText)) return;
        foreach (var controller in controllers)
        {
            var methods = controller.GetMethodsInfo();
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes<MessageAttribute>();
                if (attributes.Any(a => a.Message.Equals(messageText)))
                {
                    await (Task)method.Invoke(controller, null)!;
                }
            }
        }
    }

    public static async Task<bool> HandleUserAction(IEnumerable<BotController> controllers, UserActionStepInfo userActionInfo)
    {
        bool actionsCompleted = true;
        foreach (var controller in controllers)
        {
            var methods = controller.GetMethodsInfo();
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttribute<ActionStepAttribute>();
                if (attributes == null) continue;

                if (attributes.ActionName.Equals(userActionInfo.ActionName) && attributes.Step == userActionInfo.Step)
                {
                    await (Task)method.Invoke(controller, null)!;
                }

                if (attributes.ActionName.Equals(userActionInfo.ActionName) && attributes.Step > userActionInfo.Step)
                {
                    actionsCompleted = false;
                }
            }
        }

        return actionsCompleted;
    }

    private static IEnumerable<MethodInfo> GetMethodsInfo(this BotController controller)
    {
        return controller.GetType().GetMethods().Where(m => m.GetParameters().Length == 0);
    }
}