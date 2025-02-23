namespace Generator.Renderer.Internal.Field;

internal class OpaqueUntypedRecord : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.Is<GirModel.Record>(out var record) && Model.Record.IsOpaqueUntyped(record);
    }

    public RenderableField Convert(GirModel.Field field)
    {
        return new RenderableField(
            Name: Model.Field.GetName(field),
            Attribute: null,
            NullableTypeName: Model.Type.Pointer
        );
    }
}
