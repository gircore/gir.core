using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Property
{
    private static readonly HashSet<GirModel.Property> DisabledProperties = new();

    public static void Disable(GirModel.Property property)
    {
        lock (DisabledProperties)
        {
            DisabledProperties.Add(property);
        }
    }

    public static bool IsEnabled(GirModel.Property property)
    {
        //Does not need a lock as it is called only after all insertions are done.
        return !DisabledProperties.Contains(property);
    }
}
