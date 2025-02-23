using System.Collections.Generic;
using System.Linq;

namespace Generator.Model;

internal static class Class
{
    public static string GetFullyQualifiedInternalStructName(GirModel.Class @class)
        => Namespace.GetInternalName(@class.Namespace) + "." + GetInternalStructName(@class);

    public static string GetInternalStructName(GirModel.Class @class)
        => @class.Name + "Data";

    public static string GetInternalHandleName(GirModel.Class @class)
        => @class.Name + "Handle";

    public static string GetFullyQualifiedInternalHandleName(GirModel.Class @class)
        => Namespace.GetInternalName(@class.Namespace) + "." + GetInternalHandleName(@class);

    public static string GetFullyQualifiedPublicName(GirModel.Class @class)
        => Namespace.GetPublicName(@class.Namespace) + "." + @class.Name;

    public static string GetFullyQualifiedInternalName(GirModel.Class @class)
        => Namespace.GetInternalName(@class.Namespace) + "." + @class.Name;

    public static bool HidesConstructor(GirModel.Class? cls, GirModel.Constructor constructor)
    {
        if (cls is null)
            return false;

        var publicName = Constructor.GetName(constructor);
        var matchingConstructor = cls.Constructors.FirstOrDefault(c => Constructor.GetName(c) == publicName);

        if (matchingConstructor is null)
            return HidesConstructor(cls.Parent, constructor);

        GirModel.Parameter[] parameters = constructor.Parameters.ToArray();
        GirModel.Parameter[] foundParameters = matchingConstructor.Parameters.ToArray();

        if (parameters.Length != foundParameters.Length)
            return HidesConstructor(cls.Parent, constructor);

        for (var i = 0; i < parameters.Length; i++)
        {
            if (!parameters[i].AnyTypeOrVarArgs.Equals(foundParameters[i].AnyTypeOrVarArgs))
                return HidesConstructor(cls.Parent, constructor);
        }

        return true;
    }

    public static IEnumerable<GirModel.Method> DuplicateMethods(GirModel.Class cls, GirModel.Method method)
    {
        var publicName = Method.GetPublicName(method);

        return AllMethods(cls)
            .Where(x => Method.GetPublicName(x) == publicName)
            .Where(x => x != method)
            .Where(x => ParameterMatch(x.Parameters.ToArray(), method.Parameters.ToArray()));
    }

    public static IEnumerable<GirModel.Method> DuplicateMethods(GirModel.Class cls, GirModel.Constructor constructor)
    {
        var publicName = Constructor.GetName(constructor);

        return AllMethods(cls)
            .Where(x => Method.GetPublicName(x) == publicName)
            .Where(x => ParameterMatch(x.Parameters.ToArray(), constructor.Parameters.ToArray()));
    }

    public static IEnumerable<GirModel.Method> AllMethods(GirModel.Class cls)
    {
        var methods = new HashSet<GirModel.Method>();

        if (cls.Parent is not null)
            foreach (var method in AllMethods(cls.Parent))
                methods.Add(method);

        foreach (var @interface in cls.Implements)
            foreach (var method in Interface.AllMethods(@interface))
                methods.Add(method);

        return methods;
    }

    private static bool ParameterMatch(GirModel.Parameter[] p1, GirModel.Parameter[] p2)
    {
        if (p1.Length != p2.Length)
            return false;

        for (var i = 0; i < p1.Length; i++)
        {
            if (!p1[i].AnyTypeOrVarArgs.Equals(p2[i].AnyTypeOrVarArgs))
                return false;
        }

        return true;
    }

    public static bool IsInitiallyUnowned(GirModel.Class cls) => IsNamedInitiallyUnowned(cls.Name) || InheritsInitiallyUnowned(cls);

    private static bool InheritsInitiallyUnowned(GirModel.Class @class)
    {
        if (@class.Parent is null)
            return false;

        return IsNamedInitiallyUnowned(@class.Parent.Name) || InheritsInitiallyUnowned(@class.Parent);
    }

    private static bool IsNamedInitiallyUnowned(string name) => name == "InitiallyUnowned";
}
