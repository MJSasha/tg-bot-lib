using System;
using System.Linq;
using System.Reflection;
using TgBotLib.Utils.Attributes;

namespace TgBotLib.Utils
{
    public static class Extensions
    {
        public static string GetName(this Enum enumValue) => enumValue.GetType()?
            .GetMember(enumValue.ToString())?
            .First()?
            .GetCustomAttribute<EnumNameAttribute>()?.Name ?? enumValue.ToString();
        
        public static string GetRoot(this Type type) => type?
            .GetCustomAttribute<EntityRootAttribute>()?.Root ?? nameof(type);
    }
}
