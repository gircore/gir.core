using Generator.Model;

namespace Generator.Fixer.Record;

internal class DisableBrokenTypes : Fixer<GirModel.Record>
{
    public void Fixup(GirModel.Record record)
    {
        switch (record.Name)
        {
            case "Win32NetworkMonitor":
            case "Win32NetworkMonitorClass":
                Type.Disable(record);
                break;
        }
    }
}
