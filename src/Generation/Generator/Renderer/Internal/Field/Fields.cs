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
        new Field.Pointer(),
        new Field.PointerAlias(),
        new Field.PointerArray(),
        new Field.PrimitiveValueType(),
        new Field.PrimitiveValueTypeAlias(),
        new Field.PrimitiveValueTypeArray(),
        new Field.PrimitiveValueTypeArrayAlias(),
        new Field.Record(),
        new Field.RecordArray(),
        new Field.String(),
        new Field.StringArray(),
        new Field.Union(),
        new Field.UnionArray(),
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
        foreach (var converter in converters)
            if (converter.Supports(field))
                return Render(converter.Convert(field));

        throw new System.Exception($"Internal field \"{field.Name}\" of type {field.AnyTypeOrCallback} can not be rendered");
    }

    private static string Render(Field.RenderableField field)
    {
        return @$"{field.Attribute} public {field.NullableTypeName} {field.Name};";
    }
}
