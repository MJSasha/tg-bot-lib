# Telegram bot lib

[![NuGet Version](https://img.shields.io/nuget/v/TgBotLib.Core)](https://www.nuget.org/packages/TgBotLib.Core#readme-body-tab)
![GitHub License](https://img.shields.io/github/license/MJSasha/tg-bot-lib)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/MJSasha/tg-bot-lib/main.yml)

## Установка

1. Установите [nuget](https://www.nuget.org/packages/TgBotLib.Core#readme-body-tab):
    ```shell
    dotnet add package TgBotLib.Core
    ```
2. В Program.cs добавить следующую строку:
   ```csharp
   builder.Services.AddBotLibCore(options =>
   {
        options.BotToken = "2065215367:AAEKo4QKE7BmbH7JmUdL57YTPjj7YGeemzA";
        options.ExceptionsHandler = new ExceptionsHandler(); // Optional
   });
   ```

## Пример

> [!CAUTION]
> Методы контроллера, помеченные атрибутами из библиотеки, не должны иметь параметров и должны возвращать `Task`

### Пример стандартного контроллера:

```csharp
public class TestController : BotController
{
    private readonly IInlineButtonsGenerationService _buttonsGenerationService;
    private readonly IKeyboardButtonsGenerationService _keyboardButtonsGenerationService;

    private readonly string[] _sites = ["Google", "Github", "Telegram", "Wikipedia"];

    private readonly string[] _siteDescriptions =
    [
        "Google is a search engine",
        "Github is a git repository hosting",
        "Telegram is a messenger",
        "Wikipedia is an open wiki"
    ];

    public TestController(IInlineButtonsGenerationService buttonsGenerationService, IKeyboardButtonsGenerationService keyboardButtonsGenerationService)
    {
        _buttonsGenerationService = buttonsGenerationService;
        _keyboardButtonsGenerationService = keyboardButtonsGenerationService;
    }

    [Message("Test")]
    [Message(@"Test\d", isPattern: true)]
    public Task TestMessage()
    {
        _keyboardButtonsGenerationService.SetKeyboardButtons("Test", "Test1", "Buttons");
        return Client.SendTextMessageAsync(Update.GetChatId(),
            "Test message",
            replyMarkup: _keyboardButtonsGenerationService.GetButtons());
    }

    [Callback(nameof(TestCallback))]
    [Callback(@"\d", isPattern: true)]
    public Task TestCallback()
    {
        return Client.SendTextMessageAsync(Update.GetChatId(), "Test callback");
    }

    [Message("Buttons", ignoreCase: true)]
    public Task TestWithButtons()
    {
        _buttonsGenerationService.SetInlineButtons("Test", "2", "3");
        return Client.SendTextMessageAsync(Update.GetChatId(),
            "Test buttons",
            replyMarkup: _buttonsGenerationService.GetButtons());
    }

    [InlineQuery]
    public Task TestInlineQuery()
    {
        var results = new List<InlineQueryResult>();

        var counter = 0;
        foreach (var site in _sites.Where(s => s.ToLower().Contains(Update.InlineQuery.Query.ToLower())))
        {
            results.Add(new InlineQueryResultArticle($"{counter}", site, new InputTextMessageContent(_siteDescriptions[counter])));
            counter++;
        }

        return Client.AnswerInlineQueryAsync(Update.InlineQuery.Id, results);
    }

    [UnknownMessage]
    public Task TestUnknownMessage()
    {
        return Client.SendTextMessageAsync(Update.GetChatId(), "Hmm... 🤔");
    }

    [UnknownUpdate]
    public Task TestUnknownUpdate()
    {
        return Client.SendTextMessageAsync(Update.GetChatId(), "HMM... 🤔");
    }
}
```

### Пример контроллера с обработкой последовательности:

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

### Пример обработчика исключений

```csharp
public class ExceptionsHandler : IExceptionsHandler
{
    public Task Handle(Exception ex, ITelegramBotClient botClient, Update update)
    {
        return botClient.SendTextMessageAsync(update.GetChatId(), ex.ToString());
    }

    public Task Handle(Exception exception, ITelegramBotClient botClient)
    {
        return Task.CompletedTask;
    }
}
```
