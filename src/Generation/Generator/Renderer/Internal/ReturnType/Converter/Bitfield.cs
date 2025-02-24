namespace Generator.Renderer.Internal.ReturnType;

internal class Bitfield : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.Bitfield>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Bitfield) returnType.AnyType.AsT0;
        return new RenderableReturnType(Model.ComplexType.GetFullyQualified(type));
    }
}
