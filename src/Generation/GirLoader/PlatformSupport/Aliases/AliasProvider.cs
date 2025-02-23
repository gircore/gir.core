using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class AliasProvider
{
    public static IEnumerable<GirModel.Alias> GetAliases(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Alias>();

        if (handler.LinuxNamespace is not null)
            foreach (var alias in handler.LinuxNamespace.Aliases)
                dataStore.AddLinuxElement(alias.Name, alias);

        if (handler.MacosNamespace is not null)
            foreach (var alias in handler.MacosNamespace.Aliases)
                dataStore.AddMacosElement(alias.Name, alias);

        if (handler.WindowsNamespace is not null)
            foreach (var alias in handler.WindowsNamespace.Aliases)
                dataStore.AddWindowsElement(alias.Name, alias);

        return dataStore.Select(x => new Alias(
            linuxAlias: x.Linux,
            macosAlias: x.Macos,
            windowsAlias: x.Windows
        ));
    }
}
