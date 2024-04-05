# **<p align="center">Telegrem bot lib</p>**

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
    [Message("Test")]
    [Message("Second test")]
    public async Task Test()
    {
        await Client.SendTextMessageAsync(BotContext.Update.GetChatId(), "Test message");
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