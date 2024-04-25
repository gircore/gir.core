using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Alias
{
    private static readonly HashSet<GirModel.Alias> DisabledAliases = new();

    public static void Disable(GirModel.Alias alias)
    {
        lock (DisabledAliases)
        {
            DisabledAliases.Add(alias);
        }
    }

    public static bool IsEnabled(GirModel.Alias alias)
    {
        //Does not need a lock as it is called only after all insertions are done.
        return !DisabledAliases.Contains(alias);
    }
}
