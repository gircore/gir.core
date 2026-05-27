namespace Generator.Renderer.Internal.Field;

internal class OpaqueTypedRecord : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.Is<GirModel.Record>(out var record) && Model.Record.IsOpaqueTyped(record);
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.Type.Pointer,
            Array: null
        )];
    }
}
