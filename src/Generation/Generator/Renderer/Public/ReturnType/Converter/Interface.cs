using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class Interface : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Interface) returnType.AnyType.AsT0) + Nullable.Render(returnType);

        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.Is<GirModel.Interface>();
}
