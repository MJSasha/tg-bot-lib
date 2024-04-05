using System.Reflection;
using TgBotLib.Core.Base;

namespace TgBotLib.Core;

internal static class UpdateHandlingHelper
{
    internal static async Task HandleMessage(IEnumerable<BotController> controllers, string messageText)
    {
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

    private static IEnumerable<MethodInfo> GetMethodsInfo(this BotController controller)
    {
        return controller.GetType().GetMethods().Where(m => m.GetParameters().Length == 0);
    }
}