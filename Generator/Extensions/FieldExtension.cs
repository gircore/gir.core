using System.Text;
using Repository.Model;

namespace Generator
{
    internal static class FieldExtension
    {
        public static string WriteNative(this Field field, Namespace currentNamespace)
        {
            string type = field switch
            {
                {Callback: {} c} => c.SymbolName,
                _ => field.WriteNativeType(currentNamespace)
            };

            var builder = new StringBuilder();
            builder.Append(field.WriteNativeSummary());

            if (type == "string")
                builder.AppendLine($"[MarshalAs(UnmanagedType.LPStr)]");

            var accessibility = field.Private ? "private" : "public";
            
            builder.AppendLine($"{accessibility} {type} {field.ManagedName};");
            return builder.ToString();
        }
    }
}
