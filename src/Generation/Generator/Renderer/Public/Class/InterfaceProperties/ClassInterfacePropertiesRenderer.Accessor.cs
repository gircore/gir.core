using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassInterfaceProperties
{
    private static string RenderAccessor(GirModel.Interface @interface, GirModel.Property property)
    {
        if (property is { Readable: false, Writeable: false })
            return string.Empty;

        var builder = new StringBuilder();
        builder.AppendLine($"public {Property.GetNullableTypeName(property)} {Property.GetName(property)}");
        builder.AppendLine("{");

        if (property.Readable)
            builder.AppendLine($"    get => {GetGetter(@interface, property)};");

        if (property is { Writeable: true, ConstructOnly: false })
            builder.AppendLine($"    set => {GetSetter(@interface, property)};");

        builder.AppendLine("}");

        return builder.ToString();
    }

    private static string GetGetter(GirModel.Interface @interface, GirModel.Property property)
    {
        return Property.SupportsAccessorGetMethod(property, out var getter)
            ? $"{Namespace.GetPublicName(@interface.Namespace)}.Internal.{@interface.Name}.{Method.GetInternalName(getter)}(Handle)"
            : $"GetProperty({Namespace.GetPublicName(@interface.Namespace)}.{@interface.Name}.{Property.GetDescriptorName(property)})";
    }

    private static string GetSetter(GirModel.Interface @interface, GirModel.Property property)
    {
        return Property.SupportsAccessorSetMethod(property, out var setter)
            ? $"{Namespace.GetPublicName(@interface.Namespace)}.Internal.{@interface.Name}.{Method.GetInternalName(setter)}(Handle, value)"
            : $"SetProperty({Namespace.GetPublicName(@interface.Namespace)}.{@interface.Name}.{Property.GetDescriptorName(property)}, value)";
    }
}
