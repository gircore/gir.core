namespace Generator.Renderer.Internal.Field;

internal class PointerArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickT0(out var anyTypeReference, out _)
               && (anyTypeReference.ReferencesArray<GirModel.Pointer>() || anyTypeReference.ReferencesArray<GirModel.UnsignedPointer>());
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsT0.AsT1;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.ArrayType.GetTypeName(arrayTypeReference),
            Array: new  (arrayTypeReference.FixedSize, Model.ArrayType.GetDimensions(arrayTypeReference))
        )];
    }
}
