using Microsoft.Extensions.DependencyInjection;
using TgBotLib.Core.Base;
using TgBotLib.Core.Models;

namespace TgBotLib.Core.Services;

internal class BotControllerFactory
{
    private readonly IServiceProvider _servicesProvider;

    public BotControllerFactory(IServiceProvider servicesProvider)
    {
        _servicesProvider = servicesProvider;
    }

    internal IEnumerable<BotController> GetControllers(BotExecutionContext botExecutionContext)
    {
        var controllers = _servicesProvider.GetRequiredService<IEnumerable<BotController>>().ToList();
        controllers.ForEach(c => c.BotContext = botExecutionContext);
        return controllers;
    }
}