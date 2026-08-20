using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class Interface : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Interface) returnType.AnyTypeReference.AsT0.Type) + Nullable.Render(returnType);

        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyTypeReference.References<GirModel.Interface>();
}
