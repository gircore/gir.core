using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class FunctionProvider
{
    public static IEnumerable<GirModel.Function> GetFunctions(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Function>();

        if (handler.LinuxNamespace is not null)
            foreach (var function in handler.LinuxNamespace.Functions)
                dataStore.AddLinuxElement(function.Name, function);

        if (handler.MacosNamespace is not null)
            foreach (var function in handler.MacosNamespace.Functions)
                dataStore.AddMacosElement(function.Name, function);

        if (handler.WindowsNamespace is not null)
            foreach (var function in handler.WindowsNamespace.Functions)
                dataStore.AddWindowsElement(function.Name, function);

        return dataStore.Select(x => new Function(
            linuxFunction: x.Linux,
            macosFunction: x.Macos,
            windowsFunction: x.Windows
        ));
    }
}
