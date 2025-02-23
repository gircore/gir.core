namespace Generator.Renderer.Public.Field;

internal class Bitfield : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.Is<GirModel.Bitfield>();
    }

    public RenderableField Convert(GirModel.Field field)
    {
        var name = Model.Field.GetName(field);
        return new RenderableField(
            Name: name,
            NullableTypeName: GetNullableTypeName(field),
            SetExpression: SetExpression,
            GetExpression: GetExpression
        );
    }

    private static string GetNullableTypeName(GirModel.Field field)
    {
        var type = (GirModel.Bitfield) field.AnyTypeOrCallback.AsT0.AsT0;
        return field.IsPointer
            ? Model.Type.Pointer
            : Model.ComplexType.GetFullyQualified(type);
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

