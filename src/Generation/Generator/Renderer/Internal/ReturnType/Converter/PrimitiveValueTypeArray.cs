namespace Generator.Renderer.Internal.ReturnType;

internal class PrimitiveValueTypeArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.ReferencesArray<GirModel.PrimitiveValueType>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Model.ArrayType.GetName(returnType.AnyTypeReference.AsT1));
    }
}
