using System.Text;
using GirLoader.Output.Model;

namespace Generator
{
    internal static class FieldExtension
    {
        public static string WriteNative(this Field field, Namespace currentNamespace)
        {
            string type = field switch
            {
                { Callback: { } c } => c.SymbolName,

                // A native field which points to a record should never be a safehandle but always an IntPtr
                { TypeInformation: { IsPointer: true }, TypeReference: { ResolvedType: Record { } } } => "IntPtr",

                _ => field.WriteType(Target.Native, currentNamespace)
            };

            var builder = new StringBuilder();
            builder.Append(field.WriteNativeSummary());

            if (field.TypeInformation.Array is { FixedSize: { } fixedSize })
                builder.AppendLine($"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {fixedSize})]");

            if (type == "string")
                builder.AppendLine($"[MarshalAs(UnmanagedType.LPStr)]");

            builder.AppendLine($"{GetAccessibility(field)} {type} {field.SymbolName};");
            return builder.ToString();
        }

        private static string GetAccessibility(Field field)
        {
            return field.Private ? "public" : "public"; //TODO "public";
        }
    }
}
