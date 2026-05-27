using Generator.Model;

namespace Generator.Renderer.Internal.Field;

internal class ClassArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.IsArray<GirModel.Class>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: ArrayType.GetTypeName(arrayType),
            Array: new (arrayType.FixedSize, ArrayType.GetDimensions(arrayType))
        )];
    }
}
