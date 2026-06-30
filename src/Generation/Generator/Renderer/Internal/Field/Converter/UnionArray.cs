namespace Generator.Renderer.Internal.Field;

internal class UnionArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.IsArray<GirModel.Union>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;

        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: GetTypeName(field),
            Array: new (arrayType.FixedSize, Model.ArrayType.GetDimensions(arrayType))
        )];
    }

    private static string GetTypeName(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;
        var type = (GirModel.Union) arrayType.AnyType.AsT0;
        return Model.Union.GetFullyQualifiedInternalStructName(type);
    }
}
