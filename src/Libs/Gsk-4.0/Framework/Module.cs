using System.Runtime.CompilerServices;

namespace Gsk
{
    internal partial class Module
    {
        [ModuleInitializer]
        internal static void Initialize()
        {
            InitializeDllImport();
            RegisterTypes();
        }

        static partial void InitializeDllImport();
        static partial void RegisterTypes();
    }
}
