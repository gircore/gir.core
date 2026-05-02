namespace Generator.Renderer.Internal.Field;

internal class UntypedRecordArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.IsArray<GirModel.Record>(out var record) && Model.Record.IsUntyped(record); ;
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;
        var type = (GirModel.Record) arrayType.AnyType.AsT0;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.UntypedRecord.GetFullyQuallifiedDataName(type),
            Array: new (arrayType.FixedSize, Model.ArrayType.GetDimensions(arrayType))
        )];
    }
}
