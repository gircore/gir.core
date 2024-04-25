using GirLoader.PlatformSupport;

namespace GirTool;

internal static class PlatformGenerator
{
    public static void Fixup(Namespace @namespace)
    {
        Generator.Fixer.Namespace.Fixup(@namespace);
        Generator.Fixer.Bitfields.Fixup(@namespace.Bitfields);
        Generator.Fixer.Classes.Fixup(@namespace.Classes);
        Generator.Fixer.Records.Fixup(@namespace.Records);
        Generator.Fixer.Aliases.Fixup(@namespace.Aliases);
    }

    public static void Generate(Namespace @namespace, string path)
    {
        Generator.Framework.Generate(@namespace, path);
        Generator.Aliases.Generate(@namespace.Aliases, path);
        Generator.Classes.Generate(@namespace.Classes, path);
        Generator.Enumerations.Generate(@namespace.Enumerations, path);
        Generator.Bitfields.Generate(@namespace.Bitfields, path);
        Generator.Records.Generate(@namespace.Records, path);
        Generator.Unions.Generate(@namespace.Unions, path);
        Generator.Callbacks.Generate(@namespace.Callbacks, path);
        Generator.Constants.Generate(@namespace.Constants, path);
        Generator.Functions.Generate(@namespace.Functions, path);
        Generator.Interfaces.Generate(@namespace.Interfaces, path);

        Generator.PlatformSupport.GeneratePlatform(
            linuxNamespace: @namespace.GetPlatformHandler().LinuxNamespace,
            macosNamespace: @namespace.GetPlatformHandler().MacosNamespace,
            windowsNamespace: @namespace.GetPlatformHandler().WindowsNamespace,
            path: path
        );
    }
}
