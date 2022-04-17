using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public static class RecordProvider
{
    public static IEnumerable<GirModel.Record> GetRecords(this PlatformHandler handler)
    {
        var dataStore = new PlatformDataStore<GirModel.Record>();

        if (handler.LinuxNamespace is not null)
            foreach (var record in handler.LinuxNamespace.Records)
                dataStore.AddLinuxElement(record.Name, record);

        if (handler.MacosNamespace is not null)
            foreach (var record in handler.MacosNamespace.Records)
                dataStore.AddMacosElement(record.Name, record);

        if (handler.WindowsNamespace is not null)
            foreach (var record in handler.WindowsNamespace.Records)
                dataStore.AddWindowsElement(record.Name, record);

        return dataStore.Select(x => new Record(
            linuxRecord: x.Linux,
            macosRecord: x.Macos,
            windowsRecord: x.Windows
        ));
    }
}
