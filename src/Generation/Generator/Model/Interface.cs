using System.Collections.Generic;

namespace Generator.Model;

internal static class Interface
{
    public static IEnumerable<GirModel.Method> AllMethods(GirModel.Interface @interface)
    {
        //TODO: If interfaces implement other interfaces the methods of the parent interfaces must be returned, too.
        return @interface.Methods;
    }

    public static string GetImplementationName(GirModel.Interface @interface)
        => @interface.Name + "Helper";

    public static string GetFullyQualifiedImplementationName(GirModel.Interface @interface)
        => Namespace.GetPublicName(@interface.Namespace) + "." + GetImplementationName(@interface);
}
