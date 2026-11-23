namespace Generator.Renderer.Internal.Field;

internal class UnsignedLong : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.References<GirModel.UnsignedLong>();
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
        return field.IsPointer
            ? Model.Type.Pointer
            : Model.Type.GetName(field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsTypeReference.Type);
    }
}
