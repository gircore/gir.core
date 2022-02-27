using System;
using System.Collections.Generic;
using System.Text;

namespace Generator3.Renderer.Public
{
    public static class PropertyDescriptor
    {
        public static string RenderDescriptor(this Model.Public.Property property)
        {
            if (!property.Accessible)
                return string.Empty;

            var builder = new StringBuilder();
            builder.AppendLine($"public static readonly Property<{property.NullableTypeName}> {property.DescriptorName} = Property<{property.NullableTypeName}>.Register<{property.ClassName}>(");

            builder.AppendLine(string.Join($",{Environment.NewLine}    ", GetArguments(property)));

            builder.AppendLine(");");
            return builder.ToString();
        }

        private static IEnumerable<string> GetArguments(Model.Public.Property property)
        {
            var arguments = new List<string>()
            {
                $"nativeName: \"{property.NativeName}\"",
                $"managedName: nameof({property.PublicName})"
            };

            if (property.Readable)
                arguments.Add($"get: o => o.{property.PublicName}");

            if (property.Writeable && !property.ConstructOnly)
                arguments.Add($"set: (o, v) => o.{property.PublicName} = v");

            return arguments;
        }

    }
}
