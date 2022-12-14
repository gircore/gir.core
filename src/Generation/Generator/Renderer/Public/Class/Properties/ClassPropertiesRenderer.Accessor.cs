using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassProperties
{
    private static string RenderAccessor(GirModel.ComplexType complexType, GirModel.Property property)
    {
        if (property is { Readable: false, Writeable: false })
            return string.Empty;

        var builder = new StringBuilder();
        builder.AppendLine($"public {Property.GetNullableTypeName(property)} {Property.GetName(property)}");
        builder.AppendLine("{");

        if (property.Readable)
            builder.AppendLine($"    get => {GetGetter(complexType, property)};");

        if (property is { Writeable: true, ConstructOnly: false })
            builder.AppendLine($"    set => {GetSetter(complexType, property)};");

        builder.AppendLine("}");

        return builder.ToString();
    }

    private static string GetGetter(GirModel.ComplexType complexType, GirModel.Property property)
    {
        return Property.SupportsAccessorGetMethod(property, out var getter)
            ? $"{Namespace.GetPublicName(complexType.Namespace)}.Internal.{complexType.Name}.{Method.GetInternalName(getter)}(Handle)"
            : $"GetProperty({Property.GetDescriptorName(property)})";
    }

    private static string GetSetter(GirModel.ComplexType complexType, GirModel.Property property)
    {
        return Property.SupportsAccessorSetMethod(property, out var setter)
            ? $"{Namespace.GetPublicName(complexType.Namespace)}.Internal.{complexType.Name}.{Method.GetInternalName(setter)}(Handle, value)"
            : $"SetProperty({Property.GetDescriptorName(property)}, value)";
    }
}
