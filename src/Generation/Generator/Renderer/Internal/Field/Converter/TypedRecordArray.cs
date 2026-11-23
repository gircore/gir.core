namespace Generator.Renderer.Internal.Field;

internal class TypedRecordArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.ReferencesArray<GirModel.Record>(out var record) && Model.Record.IsTyped(record);
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsArrayTypeReference;
        var type = (GirModel.Record) arrayTypeReference.AnyTypeReference.AsTypeReference.Type;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.TypedRecord.GetFullyQuallifiedDataName(type),
            Array: new (arrayTypeReference.FixedSize, Model.ArrayType.GetDimensions(arrayTypeReference))
        )];
    }
}
