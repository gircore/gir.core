using System.Collections.Generic;

namespace Generator.Model;

internal static partial class Method
{
    private static readonly Dictionary<GirModel.Method, string> FixedPublicNames = new();
    private static readonly Dictionary<GirModel.Method, string> FixedInternalNames = new();

    public static string GetInternalName(GirModel.Method method)
    {
        return FixedInternalNames.TryGetValue(method, out var value)
            ? value
            : method.Name.ToPascalCase().EscapeIdentifier();
    }

    internal static void SetInternalName(GirModel.Method method, string name)
    {
        FixedInternalNames[method] = name;
    }

    public static string GetPublicName(GirModel.Method method)
    {
        return FixedPublicNames.TryGetValue(method, out var value)
            ? value
            : method.Name.ToPascalCase().EscapeIdentifier();
    }

    internal static void SetPublicName(GirModel.Method method, string name)
    {
        FixedPublicNames[method] = name;
    }
}
