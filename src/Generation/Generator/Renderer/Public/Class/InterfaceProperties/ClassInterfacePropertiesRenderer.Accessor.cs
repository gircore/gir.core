using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassInterfaceProperties
{
    private static string RenderAccessor(GirModel.Interface @interface, GirModel.Property property)
    {
        if (property is { Readable: false, Writeable: false })
            return string.Empty;

        var modifier = "public ";
        var explicitImplementation = string.Empty;
        if (Property.GetImplemnetExplicitly(property))
        {
            modifier = string.Empty;
            explicitImplementation = $"{Namespace.GetPublicName(@interface.Namespace)}.{@interface.Name}.";
        }

        var builder = new StringBuilder();
        builder.AppendLine($"{modifier}{Property.GetNullableTypeName(property)} {explicitImplementation}{Property.GetName(property)}");
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
            ? $"{Namespace.GetInternalName(@interface.Namespace)}.{@interface.Name}.{Method.GetInternalName(getter)}(Handle.DangerousGetHandle())"
            : $"{ComplexType.GetFullyQualified(@interface)}.{Property.GetDescriptorName(property)}.Get(this)";
    }

    private static string GetSetter(GirModel.Interface @interface, GirModel.Property property)
    {
        return Property.SupportsAccessorSetMethod(property, out var setter)
            ? $"{Namespace.GetInternalName(@interface.Namespace)}.{@interface.Name}.{Method.GetInternalName(setter)}(Handle.DangerousGetHandle(), value)"
            : $"{ComplexType.GetFullyQualified(@interface)}.{Property.GetDescriptorName(property)}.Set(this, value)";
    }
}
