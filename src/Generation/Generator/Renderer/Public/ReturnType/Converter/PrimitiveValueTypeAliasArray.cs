using System;

namespace Generator.Renderer.Public.ReturnType;

internal class PrimitiveValueTypeAliasArray : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        if (!returnType.IsPointer)
            throw new NotImplementedException("Only primitive value types alias arrays which are pointer based are supported.");

        var alias = (GirModel.Alias) returnType.AnyTypeReference.AsT1.AnyTypeReference.AsT0.Type;
        return new RenderableReturnType($"{Model.Namespace.GetPublicName(alias.Namespace)}.{Model.ArrayType.GetName(returnType.AnyTypeReference.AsT1)}");
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyTypeReference.ReferencesArrayAlias<GirModel.PrimitiveValueType>();
}
