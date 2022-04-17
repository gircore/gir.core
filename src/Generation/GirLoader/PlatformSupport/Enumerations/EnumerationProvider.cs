using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class EnumerationProvider
{
    public static IEnumerable<GirModel.Enumeration> GetEnumerations(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Enumeration>();

        if (handler.LinuxNamespace is not null)
            foreach (var enumeration in handler.LinuxNamespace.Enumerations)
                dataStore.AddLinuxElement(enumeration.Name, enumeration);

        if (handler.MacosNamespace is not null)
            foreach (var enumeration in handler.MacosNamespace.Enumerations)
                dataStore.AddMacosElement(enumeration.Name, enumeration);

        if (handler.WindowsNamespace is not null)
            foreach (var enumeration in handler.WindowsNamespace.Enumerations)
                dataStore.AddWindowsElement(enumeration.Name, enumeration);

        return dataStore.Select(x => new Enumeration(
            linuxEnumeration: x.Linux,
            macosEnumeration: x.Macos,
            windowsEnumeration: x.Windows
        ));
    }
}
