using System.Text;
using Repository.Model;

namespace Generator
{
    internal static class FieldExtension
    {
        public static string WriteNative(this Field field, Namespace currentNamespace)
        {
            var type = ((Type) field).WriteNativeType(currentNamespace);

            var builder = new StringBuilder();
            builder.Append(field.WriteNativeSummary());

            if (type == "string")
                builder.AppendLine($"[MarshalAs(UnmanagedType.LPStr)]");

            builder.AppendLine($"public {type} {field.ManagedName};");
            return builder.ToString();
        }
    }
}
