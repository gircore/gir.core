using System;
using System.Text;
using Repository.Model;

namespace Generator
{
    internal static class MethodExtension
    {
        public static string WriteNative(this Method? method, Namespace currentNamespace)
        {
            if (method is null )
                return string.Empty;


            var returnValue = method.ReturnValue.WriteNative(currentNamespace);

            var useSafeHandle = !method.IsFree() && !method.IsUnref();
            var summaryText = WriteNativeSummary(method);
            var dllImportText = $"[DllImport(\"{currentNamespace.Name}\", EntryPoint = \"{method.Name}\")]\r\n";
            var methodText = $"public static extern {returnValue} {method.SymbolName}({method.ParameterList.WriteNative(currentNamespace, useSafeHandle)});\r\n";

            return summaryText + dllImportText + methodText;
        }
        
        public static string WriteNativeSummary(Method method)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"/// <summary>");
            builder.AppendLine($"/// Calls native method {method.Name}.");
            builder.AppendLine($"/// </summary>");

            foreach (var argument in method.ParameterList.GetParameters())
            {
                builder.AppendLine($"/// <param name=\"{argument.SymbolName}\">Transfer ownership: {argument.Transfer} Nullable: {argument.Nullable}</param>");
            }

            builder.AppendLine($"/// <returns>Transfer ownership: {method.ReturnValue.Transfer} Nullable: {method.ReturnValue.Nullable}</returns>");

            return builder.ToString();
        }
        
        public static bool IsUnref(this Method method) => method.SymbolName == "Unref";
        public static bool IsFree(this Method method) => method.SymbolName == "Free";
    }
}
