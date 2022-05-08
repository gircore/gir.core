using System.Linq;

namespace Generator.Model;

internal static class Class
{
    public static string GetFullyQualifiedInternalStructName(GirModel.Class @class)
        => Namespace.GetInternalName(@class.Namespace) + "." + GetInternalStructName(@class);

    public static string GetInternalStructName(GirModel.Class @class)
        => @class.Name + "Data";

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
            if (!parameters[i].AnyType.Equals(foundParameters[i].AnyType))
                return HidesConstructor(cls.Parent, constructor);
        }

        return true;
    }
}
