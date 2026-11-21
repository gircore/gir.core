namespace Generator.Renderer.Internal.Field;

internal class EnumerationArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.ReferencesArray<GirModel.Enumeration>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsT1;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.ArrayType.GetTypeName(arrayTypeReference),
            Array: new(arrayTypeReference.FixedSize, Model.ArrayType.GetDimensions(arrayTypeReference))
        )];
    }
}
