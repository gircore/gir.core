using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Generator3.Renderer.Public
{
    public static class Property
    {
        public static string Render(this Model.Public.Property property)
        {
            try
            {
                ThrowIfNotSupported(property);
                
                var builder = new StringBuilder();
                builder.AppendLine(property.RenderDescriptor());
                builder.AppendLine(property.RenderProperty());

                return builder.ToString();
            }
            catch (Exception ex)
            {
                Log.Warning($"Did not generate property '{property.ClassName}.{property.ManagedName}': {ex.Message}");
                return string.Empty;
            }
        }

        //TODO: Remove this method if all cases are supported
        private static void ThrowIfNotSupported(Model.Public.Property property)
        {
            //TODO: Completely disabled because of naming conflicts.
            //if (property.IsPrimitiveType)
            //    return;

            throw new Exception($"Property {property.ClassName}.{property.ManagedName} is not supported");
        }
        
        private static string RenderDescriptor(this Model.Public.Property property)
            => @$"
public static readonly Property<{property.NullableTypeName}> {property.DescriptorName} = Property<{property.NullableTypeName}>.Register<{property.ClassName}>(
    {GetArguments(property)}
);";
        
        private static string GetArguments(Model.Public.Property property)
        {
            var arguments = new List<string>()
            {
                $"nativeName: \"{property.NativeName}\"",
                $"managedName: nameof({property.ManagedName})"
            };

            if (property.Readable)
                arguments.Add($"get: o => o.{property.ManagedName}");

            if (property.Writeable)
                arguments.Add($"set: (o, v) => o.{property.ManagedName} = v");

            return string.Join("," + Environment.NewLine + "    ", arguments);
        }
        
        public static string RenderProperty(this Model.Public.Property property)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public {property.NullableTypeName} {property.ManagedName}");
            builder.AppendLine("{");

            if (property.Readable)
                builder.AppendLine($"    get => GetProperty({property.DescriptorName});");

            if (property.Writeable)
                builder.AppendLine($"    set => SetProperty({property.DescriptorName}, value);");

            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
