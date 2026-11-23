namespace Generator.Renderer.Internal.Field;

internal class PrimitiveValueTypeAlias : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.ReferencesAlias<GirModel.PrimitiveValueType>();
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
            : Model.Type.GetName(((GirModel.Alias) field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsTypeReference.Type).Type);
    }
}
