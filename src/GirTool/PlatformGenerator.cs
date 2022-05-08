using Generator;
using Generator.Fixer;
using GirLoader.PlatformSupport;

namespace GirTool;

internal static class PlatformGenerator
{
    public static void Generate(PlatformHandler platformHandler, string path)
    {
        var @namespace = new Namespace(platformHandler);
        NamespaceFixer.Fixup(@namespace);
        @namespace.Generate(path);
        @namespace.Classes.Generate(path);
        @namespace.Enumerations.Generate(path);
        @namespace.Bitfields.Generate(path);
        @namespace.Records.Generate(path);
        @namespace.Unions.Generate(path);
        @namespace.Callbacks.Generate(path);
        @namespace.Constants.Generate(path);
        @namespace.Functions.Generate(path);
        @namespace.Interfaces.Generate(path);

        PlatformSupport.GeneratePlatform(
            linuxNamespace: platformHandler.LinuxNamespace,
            macosNamespace: platformHandler.MacosNamespace,
            windowsNamespace: platformHandler.WindowsNamespace,
            path: path
        );
    }
}
