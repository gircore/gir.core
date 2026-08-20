namespace Generator.Renderer.Internal.Field;

internal class UntypedRecordArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickT0(out var anyTypeReference, out _) && anyTypeReference.ReferencesArray<GirModel.Record>(out var record) && Model.Record.IsUntyped(record); ;
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsT0.AsT1;
        var type = (GirModel.Record) arrayTypeReference.AnyTypeReference.AsT0.Type;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.UntypedRecord.GetFullyQuallifiedDataName(type),
            Array: new (arrayTypeReference.FixedSize, Model.ArrayType.GetDimensions(arrayTypeReference))
        )];
    }
}
