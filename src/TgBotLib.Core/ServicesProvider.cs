using Microsoft.Extensions.DependencyInjection;
using TgBotLib.Core.Base;
using TgBotLib.Core.Services;
using TgBotLib.Coreю.Models;

namespace TgBotLib.Core;

public static class ServicesProvider
{
    public static IServiceCollection AddBotLibCore(this IServiceCollection services, Action<Options>? optionsBuilder)
    {
        var options = new Options();
        optionsBuilder?.Invoke(options);

        if (string.IsNullOrWhiteSpace(options.BotToken)) throw new ArgumentException("Invalid bot token", nameof(options.BotToken));

        services
            .AddSingleton(sp => new BotControllerFactory(sp))
            .AddSingleton(new BotSettings { BotToken = options.BotToken })
            .AddSingleton<IUsersActionsService, UsersActionsService>()
            .AddTransient<IInlineButtonsGenerationService, InlineButtonsGenerationService>()
            .AddTransient<IKeyboardButtonsGenerationService, KeyboardButtonsGenerationService>()
            .AddHostedService<TelegramBotService>()
            .AddControllers()
            ;

        if (options.ExceptionsHandler != null)
        {
            services.AddSingleton(options.ExceptionsHandler);
        }
        else
        {
            services.AddSingleton<IExceptionsHandler>(new ExceptionsHandler());
        }

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
                            arguments[i] = serviceProvider.CreateScope()
                                .ServiceProvider
                                .GetRequiredService(parameters[i].ParameterType);
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