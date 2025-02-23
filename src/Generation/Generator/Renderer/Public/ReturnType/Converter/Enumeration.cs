using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class Enumeration : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Enumeration) returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.Is<GirModel.Enumeration>();
}
