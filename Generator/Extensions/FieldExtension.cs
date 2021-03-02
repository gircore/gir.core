using System.Text;
using Repository.Model;

namespace Generator
{
    public static class FieldExtension
    {
        public static string WriteNative(this Field field)
        {
            var type =  "TODOTYPEFIELD";//WriteNativeSymbolReference(field.SymbolReference);

            var builder = new StringBuilder();
            builder.Append(field.WriteNativeSummary());

            if (type == "string")
                builder.AppendLine($"[MarshalAs(UnmanagedType.LPStr)]");

            builder.AppendLine($"public {type} {field.ManagedName};");
            return builder.ToString();
        }
    }
}
