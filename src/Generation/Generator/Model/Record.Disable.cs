using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Record
{
    private static readonly HashSet<GirModel.Record> DisabledRecords = new();

    public static void Disable(GirModel.Record record)
    {
        DisabledRecords.Add(record);
    }

    public static bool IsEnabled(GirModel.Record record)
    {
        return !DisabledRecords.Contains(record);
    }
}
