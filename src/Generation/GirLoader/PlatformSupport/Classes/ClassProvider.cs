using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class ClassProvider
{
    public static IEnumerable<GirModel.Class> GetClasses(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Class>();

        if (handler.LinuxNamespace is not null)
            foreach (var cls in handler.LinuxNamespace.Classes)
                dataStore.AddLinuxElement(cls.Name, cls);

        if (handler.MacosNamespace is not null)
            foreach (var cls in handler.MacosNamespace.Classes)
                dataStore.AddMacosElement(cls.Name, cls);

        if (handler.WindowsNamespace is not null)
            foreach (var cls in handler.WindowsNamespace.Classes)
                dataStore.AddWindowsElement(cls.Name, cls);

        return dataStore.Select(x => new Class(
           linuxClass: x.Linux,
           macosClass: x.Macos,
           windowsClass: x.Windows
        ));
    }
}
