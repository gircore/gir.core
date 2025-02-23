using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class CallbackProvider
{
    public static IEnumerable<GirModel.Callback> GetCallbacks(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Callback>();

        if (handler.LinuxNamespace is not null)
            foreach (var callback in handler.LinuxNamespace.Callbacks)
                dataStore.AddLinuxElement(callback.Name, callback);

        if (handler.MacosNamespace is not null)
            foreach (var callback in handler.MacosNamespace.Callbacks)
                dataStore.AddMacosElement(callback.Name, callback);

        if (handler.WindowsNamespace is not null)
            foreach (var callback in handler.WindowsNamespace.Callbacks)
                dataStore.AddWindowsElement(callback.Name, callback);

        return dataStore.Select(x => new Callback(
            linuxCallback: x.Linux,
            macosCallback: x.Macos,
            windowsCallback: x.Windows
        ));
    }
}
