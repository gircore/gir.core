using System;
using System.Collections.Generic;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class InterfaceProperties
{
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
