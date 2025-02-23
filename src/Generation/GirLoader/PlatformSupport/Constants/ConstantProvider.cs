using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class ConstantProvider
{
    public static IEnumerable<GirModel.Constant> GetConstants(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Constant>();

        if (handler.LinuxNamespace is not null)
            foreach (var constant in handler.LinuxNamespace.Constants)
                dataStore.AddLinuxElement(constant.Name, constant);

        if (handler.MacosNamespace is not null)
            foreach (var constant in handler.MacosNamespace.Constants)
                dataStore.AddMacosElement(constant.Name, constant);

        if (handler.WindowsNamespace is not null)
            foreach (var constant in handler.WindowsNamespace.Constants)
                dataStore.AddWindowsElement(constant.Name, constant);

        return dataStore.Select(x => new Constant(
            linuxConstant: x.Linux,
            macosConstant: x.Macos,
            windowsConstant: x.Windows
        ));
    }
}
