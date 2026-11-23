namespace Generator.Renderer.Internal.Field;

internal class UnionArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.ReferencesArray<GirModel.Union>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsArrayTypeReference;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: GetTypeName(field),
            Array: new (arrayTypeReference.FixedSize, Model.ArrayType.GetDimensions(arrayTypeReference))
        )];
    }

    private static string GetTypeName(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsArrayTypeReference;
        var type = (GirModel.Union) arrayTypeReference.AnyTypeReference.AsTypeReference.Type;
        return Model.Union.GetFullyQualifiedInternalStructName(type);
    }
}
