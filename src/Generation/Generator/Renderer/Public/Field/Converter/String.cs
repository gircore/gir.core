using Generator.Model;

namespace Generator.Renderer.Public.Field;

internal class String : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.Is<GirModel.String>();
    }

    public RenderableField Convert(GirModel.Field field)
    {
        //Struct fields are always nullable as there are no information on nullability

        return new RenderableField(
            Name: Model.Field.GetName(field),
            NullableTypeName: $"{Type.GetName(field.AnyTypeOrCallback.AsT0.AsT0)}?",
            SetExpression: SetExpression,
            GetExpression: GetExpression
        );
    }

    private static string SetExpression(GirModel.Record record, GirModel.Field field)
    {
        return $"Handle.Set{Model.Field.GetName(field)}(GLib.Internal.StringHelper.StringToPtrUtf8(value))";
    }

    private static string GetExpression(GirModel.Record record, GirModel.Field field)
    {
        return $"Marshal.PtrToStringUTF8(Handle.Get{Model.Field.GetName(field)}())";
    }
}
