using System.Text;

namespace Generator3.Renderer.Public
{
    public static class PropertyAccessor
    {
        public static string RenderAccessor(this Model.Public.Property property)
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
