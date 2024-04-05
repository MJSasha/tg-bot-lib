using Microsoft.Extensions.DependencyInjection;
using TgBotLib.Core.Base;
using TgBotLib.Core.Models;

namespace TgBotLib.Core.Services;

internal class BotControllerFactory
{
    private readonly IServiceProvider _servicesProvider;
    private readonly BotExecutionContext _botExecutionContext;

    public BotControllerFactory(IServiceProvider servicesProvider, BotExecutionContext botExecutionContext)
    {
        _servicesProvider = servicesProvider;
        _botExecutionContext = botExecutionContext;
    }

    internal IEnumerable<BotController> GetControllers()
    {
        var controllers = _servicesProvider.GetRequiredService<IEnumerable<BotController>>().ToList();
        controllers.ForEach(c => c.BotContext = _botExecutionContext);
        return controllers;
    }
}