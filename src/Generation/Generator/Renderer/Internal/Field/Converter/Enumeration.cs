namespace Generator.Renderer.Internal.Field;

internal class Enumeration : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.Is<GirModel.Enumeration>();
    }

    public RenderableField Convert(GirModel.Field field)
    {
        return new RenderableField(
            Name: Model.Field.GetName(field),
            Attribute: null,
            NullableTypeName: GetNullableTypeName(field)
        );
    }

    private static string GetNullableTypeName(GirModel.Field field)
    {
        var type = (GirModel.Enumeration) field.AnyTypeOrCallback.AsT0.AsT0;
        return field.IsPointer
            ? Model.Type.Pointer
            : Model.ComplexType.GetFullyQualified(type);
    }

}
