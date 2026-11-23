namespace Generator.Renderer.Internal.Field;

internal class Union : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.References<GirModel.Union>();
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
        var type = (GirModel.Union) field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsTypeReference.Type;
        return field.IsPointer
            ? Model.Type.Pointer
            : Model.Union.GetFullyQualifiedInternalStructName(type);
    }
}
