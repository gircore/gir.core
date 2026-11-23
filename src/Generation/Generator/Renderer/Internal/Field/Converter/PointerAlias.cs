namespace Generator.Renderer.Internal.Field;

internal class PointerAlias : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.ReferencesAlias<GirModel.Pointer>();
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
