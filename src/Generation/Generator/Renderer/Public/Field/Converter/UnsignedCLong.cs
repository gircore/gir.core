namespace Generator.Renderer.Public.Field;

internal class UnsignedCLong : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.Value.References<GirModel.UnsignedCLong>();
    }

    public RenderableField Convert(GirModel.Field field)
    {
        return new RenderableField(
            Name: Model.Field.GetName(field),
            NullableTypeName: GetNullableTypeName(field),
            SetExpression: SetExpression,
            GetExpression: GetExpression
        );
    }

    private static string GetNullableTypeName(GirModel.Field field)
    {
        return field.IsPointer
            ? Model.Type.Pointer
            : Model.Type.GetName(field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsTypeReference.Type);
    }

    private static string SetExpression(GirModel.Record record, GirModel.Field field)
    {
        return $"Handle.Set{Model.Field.GetName(field)}(new CULong(checked((nuint)value)))";
    }

    private static string GetExpression(GirModel.Record record, GirModel.Field field)
    {
        return $"Handle.Get{Model.Field.GetName(field)}().Value";
    }
}
