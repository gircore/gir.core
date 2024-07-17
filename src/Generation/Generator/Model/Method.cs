using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

    public static bool IsValidCopyFunction(GirModel.Method method)
    {
        return !method.Parameters.Any() && method.ReturnType.IsPointer;
    }

    public static bool IsValidFreeFunction(GirModel.Method method)
    {
        return !method.Parameters.Any() && method.ReturnType.AnyType.TryPickT0(out var type, out _) && type is GirModel.Void;
    }

    /// <summary>
    /// Whether this method hides a method with the same name and parameters from a parent class
    /// </summary>
    public static bool HidesMethod(GirModel.Method method)
    {
        if (method.Parent is not GirModel.Class cls)
        {
            return false;
        }

        var publicName = GetPublicName(method);
        return HidesMethod(cls.Parent, method, publicName);
    }

    private static bool HidesMethod(GirModel.Class? cls, GirModel.Method method, string publicName)
    {
        if (cls is null)
        {
            return HidesObjectMethod(method, publicName);
        }

        var matchingMethod = cls.Methods.FirstOrDefault(m => GetPublicName(m) == publicName);

        if (matchingMethod is null)
        {
            return HidesMethod(cls.Parent, method, publicName);
        }

        GirModel.Parameter[] parameters = method.Parameters.ToArray();
        GirModel.Parameter[] foundParameters = matchingMethod.Parameters.ToArray();

        if (parameters.Length != foundParameters.Length)
        {
            return HidesMethod(cls.Parent, method, publicName);
        }

        for (var i = 0; i < parameters.Length; i++)
        {
            if (!parameters[i].AnyTypeOrVarArgs.Equals(foundParameters[i].AnyTypeOrVarArgs))
            {
                return HidesMethod(cls.Parent, method, publicName);
            }
        }

        return true;
    }

    /// <summary>
    /// Whether this method hides a method from System.Object
    /// </summary>
    private static bool HidesObjectMethod(GirModel.Method method, string publicName)
    {
        if (method.Parameters.Any())
        {
            // We do not currently support detecting overrides of object methods that accept parameters
            return false;
        }

        var objectType = typeof(object);
        var matchingMembers = objectType.GetMember(
            publicName, BindingFlags.Instance | BindingFlags.Public);
        foreach (var matchingMember in matchingMembers)
        {
            if (matchingMember is MethodBase matchingMethod)
            {
                var parameters = matchingMethod.GetParameters();
                if (parameters.Length == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
