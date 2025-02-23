using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class UnionProvider
{
    public static IEnumerable<GirModel.Union> GetUnions(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Union>();

        if (handler.LinuxNamespace is not null)
            foreach (var union in handler.LinuxNamespace.Unions)
                dataStore.AddLinuxElement(union.Name, union);

        if (handler.MacosNamespace is not null)
            foreach (var union in handler.MacosNamespace.Unions)
                dataStore.AddMacosElement(union.Name, union);

        if (handler.WindowsNamespace is not null)
            foreach (var union in handler.WindowsNamespace.Unions)
                dataStore.AddWindowsElement(union.Name, union);

        return dataStore.Select(x => new Union(
            linuxUnion: x.Linux,
            macosUnion: x.Macos,
            windowsUnion: x.Windows
        ));
    }
}
