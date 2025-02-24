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
            ? $"{Namespace.GetInternalName(complexType.Namespace)}.{complexType.Name}.{Method.GetInternalName(getter)}(base.Handle.DangerousGetHandle())"
            : $"{Property.GetDescriptorName(property)}.Get(this)";
    }

    private static string GetSetter(GirModel.ComplexType complexType, GirModel.Property property)
    {
        return Property.SupportsAccessorSetMethod(property, out var setter)
            ? $"{Namespace.GetInternalName(complexType.Namespace)}.{complexType.Name}.{Method.GetInternalName(setter)}(base.Handle.DangerousGetHandle(), value)"
            : $"{Property.GetDescriptorName(property)}.Set(this, value)";
    }
}
