using System.Collections.Generic;
using System.Text;

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
        new Field.CLong(), //Must be before primitive value
        new Field.UnsignedCLong(), //Must be before primitive value
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
        var renderableFields = GetRenderableField(field);

        var sb = new StringBuilder();

        foreach (var renderableField in renderableFields)
            sb.AppendLine(Render(renderableField));

        return sb.ToString();
    }

    public static Field.RenderableField[] GetRenderableField(GirModel.Field field)
    {
        foreach (var converter in converters)
            if (converter.Supports(field))
                return converter.Convert(field);

        throw new System.Exception($"Internal field \"{field.Name}\" of type {field.AnyTypeOrCallback} can not be rendered");
    }

    private static string Render(Field.RenderableField renderableField)
    {
        return renderableField switch
        {
            { Array.FixedSize: not null } => ArrayFixedSize(renderableField),
            { Array: not null } => Array(renderableField),
            _ => Single(renderableField),
        };
    }

    private static string ArrayFixedSize(Field.RenderableField renderableField)
    {
        var inlineArrayName = renderableField.GetInlineArrayTypeName();

        return $$"""
                public {{inlineArrayName}} {{renderableField.Name}};
                [System.Runtime.CompilerServices.InlineArray({{renderableField.Array!.FixedSize}})]
                public struct {{inlineArrayName}}
                {
                    public {{renderableField.TypeName}} {{renderableField.Name}};
                }
                """;
    }

    private static string Array(Field.RenderableField renderableField)
    {
        return $"public {renderableField.GetArrayTypeName()} {renderableField.Name};";
    }

    private static string Single(Field.RenderableField renderableField)
    {
        return $"public {renderableField.TypeName} {renderableField.Name};";
    }
}
