using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class InterfaceProperties
{
    private static string RenderAccessor(GirModel.Interface @interface, GirModel.Property property)
    {
        if (property is { Readable: false, Writeable: false })
            return string.Empty;

        var builder = new StringBuilder();
        builder.Append($"public {Property.GetNullableTypeName(property)} {Property.GetName(property)}");
        builder.Append(" {");

        if (property.Readable)
            builder.Append(" get;");

        if (property is { Writeable: true, ConstructOnly: false })
            builder.Append(" set;");

        builder.Append(" }");

        return builder.ToString();
    }
}
