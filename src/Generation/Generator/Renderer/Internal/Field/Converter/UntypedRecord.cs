namespace Generator.Renderer.Internal.Field;

internal class UntypedRecord : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickT0(out var anyTypeReference, out _) && anyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: GetTypeName(field),
            Array: null
        )];
    }

    private static string GetTypeName(GirModel.Field field)
    {
        var type = (GirModel.Record) field.AnyTypeReferenceOrCallback.AsT0.AsT0.Type;
        return field.IsPointer
            ? Model.Type.Pointer
            : Model.Record.GetFullyQualifiedInternalStructName(type);
    }
}
