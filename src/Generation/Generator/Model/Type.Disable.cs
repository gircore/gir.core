using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Type
{
    private static readonly HashSet<GirModel.Type> Disabled = new();

    public static void Disable(GirModel.Type type)
    {
        lock (Disabled)
        {
            Disabled.Add(type);
        }
    }

    public static bool IsEnabled(GirModel.Type type)
    {
        //Does not need a lock as it is called only after all insertions are done.
        return !Disabled.Contains(type);
    }
}
