using Generator.Model;

namespace Generator.Renderer.Internal.Field;

internal class StringArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickT0(out var anyTypeReference, out _) && anyTypeReference.ReferencesArray<GirModel.String>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayTypeReference = field.AnyTypeReferenceOrCallback.AsT0.AsT1;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: ArrayType.GetTypeName(arrayTypeReference),
            Array: new (arrayTypeReference.FixedSize, ArrayType.GetDimensions(arrayTypeReference))
        )];
    }
}
