using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class InterfaceProvider
{
    public static IEnumerable<GirModel.Interface> GetInterfaces(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Interface>();

        if (handler.LinuxNamespace is not null)
            foreach (var @interface in handler.LinuxNamespace.Interfaces)
                dataStore.AddLinuxElement(@interface.Name, @interface);

        if (handler.MacosNamespace is not null)
            foreach (var @interface in handler.MacosNamespace.Interfaces)
                dataStore.AddMacosElement(@interface.Name, @interface);

        if (handler.WindowsNamespace is not null)
            foreach (var @interface in handler.WindowsNamespace.Interfaces)
                dataStore.AddWindowsElement(@interface.Name, @interface);

        return dataStore.Select(x => new Interface(
            linuxInterface: x.Linux,
            macosInterface: x.Macos,
            windowsInterface: x.Windows
        ));
    }
}
