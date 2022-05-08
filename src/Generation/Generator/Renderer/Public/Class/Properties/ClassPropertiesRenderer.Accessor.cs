using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassProperties
{
    private static string RenderAccessor(GirModel.Class cls, GirModel.Property property)
    {
        if (!property.Readable && !property.Writeable)
            return string.Empty;

        var builder = new StringBuilder();
        builder.AppendLine($"public {Property.GetNullableTypeName(property)} {Property.GetName(property)}");
        builder.AppendLine("{");

        if (property.Readable)
            builder.AppendLine($"    get => {GetGetter(cls, property)};");

        if (property.Writeable && !property.ConstructOnly)
            builder.AppendLine($"    set => {GetSetter(cls, property)};");

        builder.AppendLine("}");

        return builder.ToString();
    }

    private static string GetGetter(GirModel.Class cls, GirModel.Property property)
    {
        return Property.SupportsAccessorGetMethod(property, out var getter)
            ? $"Internal.{cls.Name}.{Method.GetInternalName(getter)}(Handle)"
            : $"GetProperty({Property.GetDescriptorName(property)})";
    }

    private static string GetSetter(GirModel.Class cls, GirModel.Property property)
    {
        return Property.SupportsAccessorSetMethod(property, out var setter)
            ? $"Internal.{cls.Name}.{Method.GetInternalName(setter)}(Handle, value)"
            : $"SetProperty({Property.GetDescriptorName(property)}, value)";
    }
}
