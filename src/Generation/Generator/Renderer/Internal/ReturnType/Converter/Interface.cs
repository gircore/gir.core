using Generator.Model;

namespace Generator.Renderer.Internal.ReturnType;

internal class Interface : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Interface>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var nullableTypeName = returnType.IsPointer
            ? Type.Pointer
            : Type.GetName(returnType.AnyTypeReference.AsT0.Type);

        return new RenderableReturnType(nullableTypeName);
    }
}
