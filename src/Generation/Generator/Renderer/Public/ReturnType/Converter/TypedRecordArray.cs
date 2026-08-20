namespace Generator.Renderer.Public.ReturnType;

internal class TypedRecordArray : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Model.ArrayType.GetName(returnType.AnyTypeReference.AsT1));
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyTypeReference.ReferencesArray<GirModel.Record>(out var record) && Model.Record.IsTyped(record);
}
