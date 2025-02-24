namespace Generator.Renderer.Internal.Field;

internal class UnsignedLong : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.Is<GirModel.UnsignedLong>();
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
        return field.IsPointer
            ? Model.Type.Pointer
            : Model.Type.GetName(field.AnyTypeOrCallback.AsT0.AsT0);
    }
}
