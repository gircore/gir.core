namespace Generator.Renderer.Internal.Field;

internal class PrimitiveValueTypeArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.ReferencesArray<GirModel.PrimitiveValueType>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsT1;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.ArrayType.GetTypeName(arrayTypeReference),
            Array: new  (arrayTypeReference.FixedSize, Model.ArrayType.GetDimensions(arrayTypeReference))
        )];
    }
}
