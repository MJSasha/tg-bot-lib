using System;

namespace TgBotLib.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityRootAttribute : Attribute
    {
        public string Root { get; set; }

        public EntityRootAttribute(string root)
        {
            Root = root;
        }
    }
}
