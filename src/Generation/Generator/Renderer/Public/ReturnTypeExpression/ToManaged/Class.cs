using System;
using Generator.Model;

namespace Generator.Renderer.Public.ReturnTypeExpression.ToManaged;

internal class Class : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Class>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        var cls = (GirModel.Class) returnType.AnyType.AsT0;

        if (!returnType.IsPointer)
            throw new NotImplementedException($"Can't convert {returnType} to managed as it is a pointer");

        return cls.Fundamental
            ? $"new {ComplexType.GetFullyQualified(cls)}({fromVariableName})"
            : $"GObject.Internal.ObjectWrapper.WrapHandle<{ReturnType.Render(returnType)}>({fromVariableName}, {Transfer.IsOwnedRef(returnType.Transfer).ToString().ToLower()})";
    }
}
