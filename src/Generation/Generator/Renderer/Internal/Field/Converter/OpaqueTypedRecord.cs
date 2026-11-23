namespace Generator.Renderer.Internal.Field;

internal class OpaqueTypedRecord : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.References<GirModel.Record>(out var record) && Model.Record.IsOpaqueTyped(record);
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
