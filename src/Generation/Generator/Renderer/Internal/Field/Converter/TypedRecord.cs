using Generator.Model;

namespace Generator.Renderer.Internal.Field;

internal class TypedRecord : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.Is<GirModel.Record>(out var record) && Model.Record.IsTyped(record);
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: GetTypeName(field),
            Array: null
        )];
    }

    private static string GetTypeName(GirModel.Field field)
    {
        var type = (GirModel.Record) field.AnyTypeOrCallback.AsT0.AsT0;
        return field.IsPointer
            ? Type.Pointer
            : Model.Record.GetFullyQualifiedInternalStructName(type);
    }
}
