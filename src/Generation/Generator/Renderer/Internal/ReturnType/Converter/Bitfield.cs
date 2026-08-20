namespace Generator.Renderer.Internal.ReturnType;

internal class Bitfield : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Bitfield>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Bitfield) returnType.AnyTypeReference.AsT0.Type;
        return new RenderableReturnType(Model.ComplexType.GetFullyQualified(type));
    }
}
