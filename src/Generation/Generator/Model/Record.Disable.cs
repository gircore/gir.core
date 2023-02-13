using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Record
{
    private static readonly HashSet<GirModel.Record> DisabledRecords = new();

    public static void Disable(GirModel.Record record)
    {
        lock (DisabledRecords)
        {
            DisabledRecords.Add(record);
        }
    }

    public static bool IsEnabled(GirModel.Record record)
    {
        //Does not need a lock as it is called only after all insertions are done.
        return !DisabledRecords.Contains(record);
    }
}
