using System.Reflection;
using System.Text.RegularExpressions;
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
                if (CheckMessageForAttribute(attributes, messageText))
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

    public static async Task HandleCallbackQuery(IEnumerable<BotController> controllers, string getQueryMessage)
    {
        if (string.IsNullOrEmpty(getQueryMessage)) return;
        foreach (var controller in controllers)
        {
            var methods = controller.GetMethodsInfo();
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes<CallbackAttribute>();
                if (CheckMessageForAttribute(attributes, getQueryMessage))
                {
                    await (Task)method.Invoke(controller, null)!;
                }
            }
        }
    }

    private static bool CheckMessageForAttribute(IEnumerable<BaseAttribute> attributes, string messageText)
    {
        return attributes.Any(a => a.IsPattern
            ? Regex.IsMatch(messageText, a.Message)
            : messageText.Equals(a.Message));
    }

    private static IEnumerable<MethodInfo> GetMethodsInfo(this BotController controller)
    {
        return controller.GetType().GetMethods().Where(m => m.GetParameters().Length == 0 && m.IsPublic);
    }
}