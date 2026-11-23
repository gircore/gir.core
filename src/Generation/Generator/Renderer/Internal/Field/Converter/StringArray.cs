using Generator.Model;

namespace Generator.Renderer.Internal.Field;

internal class StringArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.ReferencesArray<GirModel.String>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsArrayTypeReference;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: ArrayType.GetTypeName(arrayTypeReference),
            Array: new (arrayTypeReference.FixedSize, ArrayType.GetDimensions(arrayTypeReference))
        )];
    }
}
