using System;
using Generator.Model;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Interface : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Interface>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        if (!returnType.IsPointer)
            throw new NotImplementedException($"Can't convert {returnType} to managed as it is a pointer");

        var @interface = (GirModel.Interface) returnType.AnyType.AsT0;

        return $"GObject.Internal.ObjectWrapper.WrapInterfaceHandle<{Model.Interface.GetFullyQualifiedImplementationName(@interface)}>({fromVariableName}, {Transfer.IsOwnedRef(returnType.Transfer).ToString().ToLower()})";
    }
}
