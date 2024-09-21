using System;
using Type = Generator.Model.Type;

namespace Generator.Renderer.Internal.ReturnType;

internal class PrimitiveValueTypeAliasArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsArrayAlias<GirModel.PrimitiveValueType>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        if (!returnType.IsPointer)
            throw new NotImplementedException("Only primitive value types alias arrays which are pointer based are supported.");

        return new RenderableReturnType(Type.Pointer);
    }
}
