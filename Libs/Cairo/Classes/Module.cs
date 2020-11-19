using System.Runtime.CompilerServices;

namespace cairo
{
    internal partial class Module
    {
        [ModuleInitializer]
        internal static void Initialize()
        {
            RegisterTypes();
        }

        static void RegisterTypes()
        {
            RegisterClasses();
        }

        static partial void RegisterClasses();
    }
}
