using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Internal;

internal static class Fields
{
    public static string Render(IEnumerable<GirModel.Field> fields)
    {
        return fields
            .Select(RenderableFieldFactory.Create)
            .Select(Render)
            .Join(Environment.NewLine);
    }

    private static string Render(RenderableField field)
    {
        return @$"{field.Attribute} public {field.NullableTypeName} {field.Name};";
    }
}
