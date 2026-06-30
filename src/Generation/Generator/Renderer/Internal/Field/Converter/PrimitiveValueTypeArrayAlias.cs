namespace Generator.Renderer.Internal.Field;

internal class PrimitiveValueTypeArrayAlias : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.IsArrayAlias<GirModel.PrimitiveValueType>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.ArrayType.GetTypeName(arrayType, true),
            Array: new (arrayType.FixedSize, Model.ArrayType.GetDimensions(arrayType))
        )];
    }
}
