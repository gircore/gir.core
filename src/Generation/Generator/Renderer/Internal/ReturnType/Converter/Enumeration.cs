namespace Generator.Renderer.Internal.ReturnType;

internal class Enumeration : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.Enumeration>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Enumeration) returnType.AnyType.AsT0;
        return new RenderableReturnType(Model.ComplexType.GetFullyQualified(type));
    }
}
