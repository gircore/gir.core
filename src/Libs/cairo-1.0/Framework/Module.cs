using System.Runtime.CompilerServices;

namespace cairo
{
    internal partial class Module
    {
        [ModuleInitializer]
        internal static void Initialize()
        {
            // InitializeDllImport();

            // We override the normal DllImporter generated for us
            // as we need to take into account cairo being spread
            // across multiple shared libraries.
            Internal.DllImportOverride.Initialize();
            RegisterTypes();
        }

#pragma warning disable IDE0051 // IDE0051: Remove unused member
        static partial void InitializeDllImport();
#pragma warning restore IDE0051

        static partial void RegisterTypes();
    }
}
