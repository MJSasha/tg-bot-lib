using System;
using System.Linq;
using System.Reflection;
using TgBotLib.Utils.Attributes;

namespace TgBotLib.Utils
{
    public static class Extensions
    {
        public static string GetName(this Enum enumItem)
        {
            var attributes = typeof(Enum).GetCustomAttributes<EnumNameAttribute>();

            if (attributes.Any()) return attributes.First().Name;
            return enumItem.ToString();
        }
    }
}
