using System.Collections.Generic;
using System.Linq;

namespace Generator.Model;

internal static partial class Method
{
    private static readonly Dictionary<GirModel.Method, string> FixedPublicNames = new();
    private static readonly Dictionary<GirModel.Method, string> FixedInternalNames = new();
    private static readonly HashSet<GirModel.Method> ImplementExplicitly = new();

    public static string GetInternalName(GirModel.Method method)
    {
        //Does not need a lock as it is called only after all insertions are done.
        if (FixedInternalNames.TryGetValue(method, out var value))
            return value;

        if (method.Shadows is null)
            return method.Name.ToPascalCase().EscapeIdentifier();

        if (method.Parameters.Count() != method.Shadows.Parameters.Count())
            return method.Shadows.Name.ToPascalCase().EscapeIdentifier();

        if (method.Parameters.Select(x => x.AnyTypeOrVarArgs).Except(method.Shadows.Parameters.Select(x => x.AnyTypeOrVarArgs)).Any())
            return method.Shadows.Name.ToPascalCase().EscapeIdentifier();

        return method.Name.ToPascalCase().EscapeIdentifier();
    }

    internal static void SetInternalName(GirModel.Method method, string name)
    {
        lock (FixedInternalNames)
        {
            FixedInternalNames[method] = name;
        }
    }

    public static string GetPublicName(GirModel.Method method)
    {
        //Does not need a lock as it is called only after all insertions are done.
        if (FixedPublicNames.TryGetValue(method, out var value))
            return value;

        if (method.Shadows is null)
            return method.Name.ToPascalCase().EscapeIdentifier();

        if (method.Parameters.Count() != method.Shadows.Parameters.Count())
            return method.Shadows.Name.ToPascalCase().EscapeIdentifier();

        if (method.Parameters.Select(x => x.AnyTypeOrVarArgs).Except(method.Shadows.Parameters.Select(x => x.AnyTypeOrVarArgs)).Any())
            return method.Shadows.Name.ToPascalCase().EscapeIdentifier();

        return method.Name.ToPascalCase().EscapeIdentifier();
    }

    internal static void SetPublicName(GirModel.Method method, string name)
    {
        lock (FixedPublicNames)
        {
            FixedPublicNames[method] = name;
        }
    }

    public static bool GetImplemnetExplicitly(GirModel.Method method)
    {
        //Does not need a lock as it is called only after all insertions are done.
        return ImplementExplicitly.Contains(method);
    }

    internal static void SetImplementExplicitly(GirModel.Method method)
    {
        lock (ImplementExplicitly)
        {
            ImplementExplicitly.Add(method);
        }
    }
}
