namespace Generator.Renderer.Internal.ReturnType;

internal class Enumeration : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Enumeration>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Enumeration) returnType.AnyTypeReference.AsTypeReference.Type;
        return new RenderableReturnType(Model.ComplexType.GetFullyQualified(type));
    }
}
