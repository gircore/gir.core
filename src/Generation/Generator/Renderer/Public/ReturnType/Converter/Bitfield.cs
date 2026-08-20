using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class Bitfield : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Bitfield) returnType.AnyTypeReference.AsT0.Type);

        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyTypeReference.References<GirModel.Bitfield>();
}
