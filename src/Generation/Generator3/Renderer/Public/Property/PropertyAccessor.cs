using System.Text;
using Generator3.Converter;

namespace Generator3.Renderer.Public
{
    public static class PropertyAccessor
    {
        public static string RenderAccessor(this Model.Public.Property property)
        {
            if (!property.Accessible)
                return string.Empty;

            var builder = new StringBuilder();
            builder.AppendLine($"public {property.NullableTypeName} {property.PublicName}");
            builder.AppendLine("{");

            if (property.Readable)
                builder.AppendLine($"    get => {GetGetter(property)};");

            if (property.Writeable && !property.ConstructOnly)
                builder.AppendLine($"    set => {GetSetter(property)};");

            builder.AppendLine("}");

            return builder.ToString();
        }

        private static string GetGetter(Model.Public.Property property)
        {
            return property.SupportsAccessorGetMethod(out var getter)
                ? $"Internal.{property.ClassName}.{getter.GetInternalName()}(Handle)"
                : $"GetProperty({property.DescriptorName})";
        }

        private static string GetSetter(Model.Public.Property property)
        {
            return property.SupportsAccessorSetMethod(out var setter)
                ? $"Internal.{property.ClassName}.{setter.GetInternalName()}(Handle, value)"
                : $"SetProperty({property.DescriptorName}, value)";
        }
    }
}
