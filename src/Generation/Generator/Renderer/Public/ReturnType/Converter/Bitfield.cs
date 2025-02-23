using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class Bitfield : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Bitfield) returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.Is<GirModel.Bitfield>();
}
