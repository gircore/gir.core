using System;
using System.Collections.Generic;
using System.Text;

namespace Generator3.Renderer.Public
{
    public static class PropertyDescriptor
    {
        public static string RenderDescriptor(this Model.Public.Property property)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public static readonly Property<{property.NullableTypeName}> {property.DescriptorName} = Property<{property.NullableTypeName}>.Register<{property.ClassName}>(");

            builder.AppendLine(GetArguments(property));

            builder.AppendLine(");");
            return builder.ToString();
        }

        private static string GetArguments(Model.Public.Property property)
        {
            var arguments = new List<string>()
            {
                $"nativeName: \"{property.NativeName}\"",
                $"managedName: nameof({property.PublicName})"
            };

            if (property.Readable)
                arguments.Add($"get: o => o.{property.PublicName}");

            if (property.Writeable)
                arguments.Add($"set: (o, v) => o.{property.PublicName} = v");

            return string.Join($",{Environment.NewLine}    ", arguments);
        }

    }
}
