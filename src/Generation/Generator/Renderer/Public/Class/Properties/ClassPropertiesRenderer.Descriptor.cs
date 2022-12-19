using System;
using System.Collections.Generic;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassProperties
{
    private static string RenderDescriptor(GirModel.ComplexType complexType, GirModel.Property property)
    {
        if (property is { Readable: false, Writeable: false })
            return string.Empty;

        var builder = new StringBuilder();
        builder.AppendLine($"public static readonly Property<{Property.GetNullableTypeName(property)}, {complexType.Name}> {Property.GetDescriptorName(property)} = new (");

        builder.AppendLine("    " + string.Join($",{Environment.NewLine}    ", GetArguments(property)));

        builder.AppendLine(");");
        return builder.ToString();
    }

    private static IEnumerable<string> GetArguments(GirModel.Property property)
    {
        var arguments = new List<string>
        {
            $"unmanagedName: \"{property.Name}\"",
            $"managedName: nameof({Property.GetName(property)})"
        };

        if (property.Readable)
            arguments.Add($"get: o => o.{Property.GetName(property)}");

        if (property is { Writeable: true, ConstructOnly: false })
            arguments.Add($"set: (o, v) => o.{Property.GetName(property)} = v");

        return arguments;
    }
}
