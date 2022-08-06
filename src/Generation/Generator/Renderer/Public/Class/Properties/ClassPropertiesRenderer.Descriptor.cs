using System;
using System.Collections.Generic;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassProperties
{
    private static string RenderDescriptor(GirModel.Class cls, GirModel.Property property)
    {
        if (!property.Readable && !property.Writeable)
            return string.Empty;

        var builder = new StringBuilder();
        builder.AppendLine($"public static readonly Property<{Property.GetNullableTypeName(property)}> {Property.GetDescriptorName(property)} = Property<{Property.GetNullableTypeName(property)}>.Register<{cls.Name}>(");

        builder.AppendLine(string.Join($",{Environment.NewLine}    ", GetArguments(property)));

        builder.AppendLine(");");
        return builder.ToString();
    }

    private static IEnumerable<string> GetArguments(GirModel.Property property)
    {
        var arguments = new List<string>()
        {
            $"nativeName: \"{property.Name}\"",
            $"managedName: nameof({Property.GetName(property)})"
        };

        if (property.Readable)
            arguments.Add($"get: o => o.{Property.GetName(property)}");

        if (property.Writeable && !property.ConstructOnly)
            arguments.Add($"set: (o, v) => o.{Property.GetName(property)} = v");

        return arguments;
    }

}
