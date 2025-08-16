using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static class InterfaceProperties
{
    public static string RenderProperties(this GirModel.Interface @interface)
    {
        return @interface.Properties
            .Where(Property.IsEnabled)
            .Select(x => RenderProperty(@interface, x))
            .Join(Environment.NewLine);
    }

    private static string RenderProperty(GirModel.Interface @interface, GirModel.Property property)
    {
        try
        {
            Property.ThrowIfNotSupported(@interface, property);

            var builder = new StringBuilder();
            builder.AppendLine(RenderDescriptor(@interface, property));
            builder.AppendLine(RenderAccessor(@interface, property));

            return builder.ToString();
        }
        catch (Exception ex)
        {
            var message = $"Did not generate property '{@interface.Name}.{property.Name}': {ex.Message}";

            if (ex is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }

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

    private static string RenderDescriptor(GirModel.Interface @interface, GirModel.Property property)
    {
        if (property is { Readable: false, Writeable: false })
            return string.Empty;

        var builder = new StringBuilder();
        builder.AppendLine($"public static readonly Property<{Property.GetNullableTypeName(property)}, {@interface.Name}> {Property.GetDescriptorName(property)} = new (");

        var arguments = new List<string>()
        {
            $"unmanagedName: \"{property.Name}\"",
            $"managedName: nameof({Property.GetName(property)})"
        };

        builder.AppendLine("    " + string.Join($",{Environment.NewLine}    ", arguments));

        builder.AppendLine(");");
        return builder.ToString();
    }
}
