namespace TgBotLib.Core;

public static class ValidationHelper
{
    public static void ValidateCallback(string callback)
    {
        if (System.Text.Encoding.UTF8.GetByteCount(callback) > 64)
        {
            throw new ArgumentException($"Invalid callback {callback}. Callback must be less than 64 symbols", nameof(callback));
        }
    }
}