using System.Collections.Generic;

namespace Generator.Model;

internal static class Field
{
    private static readonly Dictionary<GirModel.Field, string> FixedNames = new();

    public static string GetName(GirModel.Field field)
    {
        if (FixedNames.TryGetValue(field, out var value))
            return value;

        return field.Name.ToPascalCase();
    }

    public static void SetName(GirModel.Field field, string name)
    {
        lock (FixedNames)
        {
            FixedNames[field] = name;
        }
    }
}
