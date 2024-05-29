using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class Fields
{
    private static readonly List<Field.FieldConverter> converters = new()
    {
        new Field.Bitfield(),
        new Field.Callback(),
        new Field.CallbackType(),
        new Field.CallbackTypeAlias(),
        new Field.Class(),
        new Field.ClassArray(),
        new Field.Enumeration(),
        new Field.EnumerationArray(),
        new Field.OpaqueTypedRecord(),
        new Field.OpaqueUntypedRecord(),
        new Field.Pointer(),
        new Field.PointerAlias(),
        new Field.PointerArray(),
        new Field.Long(), //Must be before primitive value
        new Field.UnsignedLong(), //Must be before primitive value
        new Field.PrimitiveValueType(),
        new Field.PrimitiveValueTypeAlias(),
        new Field.PrimitiveValueTypeArray(),
        new Field.PrimitiveValueTypeArrayAlias(),
        new Field.String(),
        new Field.StringArray(),
        new Field.TypedRecord(),
        new Field.TypedRecordArray(),
        new Field.Union(),
        new Field.UnionArray(),
        new Field.UntypedRecord(),
        new Field.UntypedRecordArray()
    };

    public static string Render(IEnumerable<GirModel.Field> fields)
    {
        var fieldList = new List<string>();

        foreach (var field in fields)
            fieldList.Add(Render(field));

        return fieldList.Join(System.Environment.NewLine);
    }

    public static string Render(GirModel.Field field)
    {
        var renderableField = GetRenderableField(field);
        return $"{renderableField.Attribute} public {renderableField.NullableTypeName} {renderableField.Name};";
    }

    public static Field.RenderableField GetRenderableField(GirModel.Field field)
    {
        foreach (var converter in converters)
            if (converter.Supports(field))
                return converter.Convert(field);

        throw new System.Exception($"Internal field \"{field.Name}\" of type {field.AnyTypeOrCallback} can not be rendered");
    }
}
