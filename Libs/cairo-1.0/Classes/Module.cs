using System.Runtime.CompilerServices;

namespace cairo
{
    internal partial class Module
    {
        [ModuleInitializer]
        internal static void Initialize()
        {
            InitializeDllImport();
        }

        static partial void InitializeDllImport();
    }
}
