# Telegram bot lib

## Пример

В Program.cs добавить следующую строку:

```csharp
builder.Services.AddBotLibCore("<YOUR_BOT_TOKEN>");
```

Далее, по аналогии, можно добавить контроллеры

Пример стандартного контроллера:

```csharp
public class TestController : BotController
{
    private readonly IInlineButtonsGenerationService _buttonsGenerationService;

    public TestController(IInlineButtonsGenerationService buttonsGenerationService)
    {
        _buttonsGenerationService = buttonsGenerationService;
    }

    [Message("Test")]
    [Message(@"Test\d", isPattern: true)]
    public Task TestMessage()
    {
        return Client.SendTextMessageAsync(BotContext.Update.GetChatId(), "Test message");
    }

    [Callback(@"Test")]
    [Callback(@"\d", isPattern: true)]
    public Task TestCallback()
    {
        return Client.SendTextMessageAsync(BotContext.Update.GetChatId(), "Test callback");
    }

    [Message("Buttons")]
    public Task TestWithButtons()
    {
        _buttonsGenerationService.SetInlineButtons("Test", "2", "3");
        return Client.SendTextMessageAsync(BotContext.Update.GetChatId(),
            "Test buttons",
            replyMarkup: _buttonsGenerationService.GetButtons());
    }

    [UnknownMessage]
    public Task TestUnknownMessage()
    {
        return Client.SendTextMessageAsync(BotContext.Update.GetChatId(), "Hmm... 🤔");
    }
}
```

Пример контроллера с обработкой последовательности:

```csharp
public class SecondTestController : BotController
{
    private readonly IUsersActionsService _usersActionsService;

    public SecondTestController(IUsersActionsService usersActionsService)
    {
        _usersActionsService = usersActionsService;
    }

    [Message("Init handling")]
    public async Task InitHandling()
    {
        _usersActionsService.HandleUser(BotContext.Update.GetChatId(), nameof(SecondTestController));
        await Client.SendTextMessageAsync(BotContext.Update.GetChatId(), "Handling inited");
    }

    [ActionStep(nameof(SecondTestController), 0)]
    public async Task FirstStep()
    {
        await Client.SendTextMessageAsync(BotContext.Update.GetChatId(), $"First step {BotContext.Update.GetMessageText()}");
    }

    [ActionStep(nameof(SecondTestController), 1)]
    public async Task SecondStep()
    {
        await Client.SendTextMessageAsync(BotContext.Update.GetChatId(), $"Second step {BotContext.Update.GetMessageText()}");
    }

    [ActionStep(nameof(SecondTestController), 2)]
    public async Task ThirdStep()
    {
        await Client.SendTextMessageAsync(BotContext.Update.GetChatId(), $"Third step {BotContext.Update.GetMessageText()}");
    }
}
```