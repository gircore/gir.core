using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Callback
{
    private static readonly HashSet<GirModel.Callback> Disabled = new();

    public static void Disable(GirModel.Callback callback)
    {
        lock (Disabled)
        {
            Disabled.Add(callback);
        }
    }

    public static bool IsEnabled(GirModel.Callback callback)
    {
        //Does not need a lock as it is called only after all insertions are done.
        return !Disabled.Contains(callback);
    }
}
