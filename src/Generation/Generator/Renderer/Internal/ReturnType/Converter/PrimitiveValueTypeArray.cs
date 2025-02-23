namespace Generator.Renderer.Internal.ReturnType;

internal class PrimitiveValueTypeArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsArray<GirModel.PrimitiveValueType>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Model.ArrayType.GetName(returnType.AnyType.AsT1));
    }
}
