using System.Collections.Generic;

namespace Generator3.Converter;

public static class RecordDisableExtension
{
    private static readonly HashSet<GirModel.Record> DisabledRecords = new();

    public static void Disable(this GirModel.Record record)
    {
        DisabledRecords.Add(record);
    }

    public static bool IsDisabled(this GirModel.Record record)
    {
        return DisabledRecords.Contains(record);
    }
}
