namespace Generator.Renderer.Internal.Field;

internal class PointerArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference)
               && (anyTypeReference.Value.ReferencesArray<GirModel.Pointer>() || anyTypeReference.Value.ReferencesArray<GirModel.UnsignedPointer>());
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsArrayTypeReference;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.ArrayType.GetTypeName(arrayTypeReference),
            Array: new  (arrayTypeReference.FixedSize, Model.ArrayType.GetDimensions(arrayTypeReference))
        )];
    }
}
