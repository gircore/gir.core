using Generator3;
using Generator3.Fixer.Public;
using GirLoader.PlatformSupport;

namespace GirTool;

internal static class PlatformGenerator
{
    public static void Generate(PlatformHandler platformHandler)
    {
        var @namespace = new Namespace(platformHandler);
        @namespace.Fixup();
        @namespace.GenerateFramework();
        @namespace.Classes.Generate();
        @namespace.Enumerations.Generate();
        @namespace.Bitfields.Generate();
        @namespace.Records.Generate();
        @namespace.Unions.Generate();
        @namespace.Callbacks.Generate();
        @namespace.Constants.Generate();
        @namespace.Functions.Generate();
        @namespace.Interfaces.Generate();

        PlatformSupport.GeneratePlatform(
            linuxNamespace: platformHandler.LinuxNamespace,
            macosNamespace: platformHandler.MacosNamespace,
            windowsNamespace: platformHandler.WindowsNamespace
        );
    }
}
