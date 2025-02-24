using System;

namespace Generator.Renderer.Public.ReturnType;

internal class PrimitiveValueTypeAliasArray : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        if (!returnType.IsPointer)
            throw new NotImplementedException("Only primitive value types alias arrays which are pointer based are supported.");

        var alias = (GirModel.Alias) returnType.AnyType.AsT1.AnyType.AsT0;
        return new RenderableReturnType($"{Model.Namespace.GetPublicName(alias.Namespace)}.{Model.ArrayType.GetName(returnType.AnyType.AsT1)}");
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.IsArrayAlias<GirModel.PrimitiveValueType>();
}
