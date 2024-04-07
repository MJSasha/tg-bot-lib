using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotLib.Core.Base;
using TgBotLib.Core.Models;
using TgBotLib.Core.Services;

namespace TgBotLib.Core;

internal class TelegramBotService : IHostedService
{
    private readonly BotControllerFactory _botControllerFactory;
    private readonly IUsersActionsService _usersActionsService;
    private readonly TelegramBotClient _botClient;

    public TelegramBotService(BotSettings botSettings, BotControllerFactory botControllerFactory, IUsersActionsService usersActionsService)
    {
        _botControllerFactory = botControllerFactory;
        _usersActionsService = usersActionsService;
        _botClient = new TelegramBotClient(botSettings.BotToken);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using CancellationTokenSource cts = new();

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        var me = await _botClient.GetMeAsync(cancellationToken: cancellationToken);

        Console.WriteLine($"Start listening for @{me.Username}");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var botExecutionContext = new BotExecutionContext().SetBotClient(botClient).SetUpdate(update);
        var controllers = _botControllerFactory.GetControllers(botExecutionContext);

        var userActionInfo = _usersActionsService.GetUserActionStepInfo(update.GetChatId());

        if (userActionInfo != null)
        {
            var actionsCompleted = await UpdateHandlingHelper.HandleUserAction(controllers, userActionInfo);
            if (actionsCompleted) _usersActionsService.RemoveUser(update.GetChatId());
            else _usersActionsService.IncrementStep(update.GetChatId());
            return;
        }

        await (update.Type switch
        {
            UpdateType.Message => HandleMessage(update, controllers),
            UpdateType.CallbackQuery => HandleCallbackQuery(update, controllers),
            UpdateType.InlineQuery => UpdateHandlingHelper.HandleUnknown<InlineQueryAttribute>(controllers),
            UpdateType.ChosenInlineResult => UpdateHandlingHelper.HandleUnknown<ChosenInlineResultAttribute>(controllers),
            _ => UpdateHandlingHelper.HandleUnknown<UnknownUpdateAttribute>(controllers)
        });
    }

    private Task HandleCallbackQuery(Update update, IEnumerable<BotController> controllers)
    {
        return UpdateHandlingHelper.HandleUpdate<CallbackAttribute>(controllers, update.GetCallbackMessage());
    }

    private Task HandleMessage(Update update, IEnumerable<BotController> controllers)
    {
        return update.Message!.Type switch
        {
            MessageType.Text => UpdateHandlingHelper.HandleUpdate<MessageAttribute>(controllers, update.GetMessageText()),
            _ => UpdateHandlingHelper.HandleUnknown<UnknownMessageAttribute>(controllers)
        };
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // TODO: Handle polling error logic here
        return Task.CompletedTask;
    }
}