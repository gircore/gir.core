using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Method
{
    private static readonly HashSet<GirModel.Method> DisabledMethods = new();

    public static void Disable(GirModel.Method method)
    {
        lock (DisabledMethods)
        {
            DisabledMethods.Add(method);
        }
    }

    public static bool IsEnabled(GirModel.Method method)
    {
        //Does not need a lock as it is called only after all insertions are done.
        return !DisabledMethods.Contains(method);
    }
}
