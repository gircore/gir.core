using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Property
{
    private static readonly HashSet<GirModel.Property> DisabledProperties = new();

    public static void Disable(GirModel.Property property)
    {
        DisabledProperties.Add(property);
    }

    public static bool IsEnabled(GirModel.Property property)
    {
        return !DisabledProperties.Contains(property);
    }
}
