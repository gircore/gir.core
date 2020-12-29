using System.Runtime.CompilerServices;

namespace GstBase
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
