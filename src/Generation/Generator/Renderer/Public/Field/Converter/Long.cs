namespace Generator.Renderer.Public.Field;

internal class Long : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.Is<GirModel.Long>();
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
            : Model.Type.GetName(field.AnyTypeOrCallback.AsT0.AsT0);
    }

    private static string SetExpression(GirModel.Record record, GirModel.Field field)
    {
        return $"Handle.Set{Model.Field.GetName(field)}(value)";
    }

    private static string GetExpression(GirModel.Record record, GirModel.Field field)
    {
        return $"Handle.Get{Model.Field.GetName(field)}()";
    }
}
