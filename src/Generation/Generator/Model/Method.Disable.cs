using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Method
{
    private static readonly HashSet<GirModel.Method> DisabledMethods = new();

    public static void Disable(GirModel.Method method)
    {
        DisabledMethods.Add(method);
    }

    public static bool IsEnabled(GirModel.Method method)
    {
        return !DisabledMethods.Contains(method);
    }
}
