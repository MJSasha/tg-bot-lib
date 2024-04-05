using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TgBotLib.Core.Base;
using TgBotLib.Core.Models;
using TgBotLib.Core.Services;

namespace TgBotLib.Core;

public static class ServicesProvider
{
    public static IServiceCollection AddBotLibCore(this IServiceCollection services, string botToken)
    {
        var botExecutionContext = new BotExecutionContext();
        var botSettings = new BotSettings { BotToken = botToken };

        services
            .AddSingleton(botExecutionContext)
            .AddSingleton(sp => new BotControllerFactory(sp, botExecutionContext))
            .AddSingleton(botSettings)
            .AddSingleton<IHostedService, TelegramBotService>()
            .AddControllers()
            ;

        return services;
    }

    private static IServiceCollection AddControllers(this IServiceCollection services)
    {
        var botControllerType = typeof(BotController);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var controllerTypes = assembly.GetTypes().Where(t => botControllerType.IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var controllerType in controllerTypes)
            {
                services.AddTransient(botControllerType, serviceProvider =>
                {
                    var constructor = controllerType.GetConstructors().FirstOrDefault();
                    if (constructor != null)
                    {
                        var parameters = constructor.GetParameters();
                        var arguments = new object[parameters.Length];
                        for (var i = 0; i < parameters.Length; i++)
                        {
                            arguments[i] = serviceProvider.GetRequiredService(parameters[i].ParameterType);
                        }

                        return (Activator.CreateInstance(controllerType, arguments) as BotController)!;
                    }
                    else
                    {
                        return Activator.CreateInstance(controllerType) as BotController;
                    }
                });
            }
        }

        return services;
    }
}