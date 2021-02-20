using System.Runtime.CompilerServices;

namespace GstPbutils
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
