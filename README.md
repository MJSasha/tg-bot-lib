# **<p align="center">Telegrem bot lib</p>**

## Пример

В Program.cs добавить следующую строку:

```csharp
builder.Services.AddBotLibCore("<YOUR_BOT_TOKEN>");
```

Далее, по аналогии, можно добавить контроллеры:

```csharp
using Telegram.Bot;
using TgBotLib.Core;
using TgBotLib.Core.Base;

namespace WebApplication2.BotControllers;

public class TestController : BotController
{
    [Message("Test")]
    [Message("Second test")]
    public async Task Test()
    {
        var client = BotContext.BotClient;
        await client.SendTextMessageAsync(BotContext.Update.Message.Chat.Id, "Test message");
    }
}
```