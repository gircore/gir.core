using Generator.Model;

namespace Generator.Renderer.Internal.Field;

internal class ClassArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.ReferencesArray<GirModel.Class>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayType = field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsArrayTypeReference;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: ArrayType.GetTypeName(arrayType),
            Array: new (arrayType.FixedSize, ArrayType.GetDimensions(arrayType))
        )];
    }
}
