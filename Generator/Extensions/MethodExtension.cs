using System;
using System.Text;
using Repository.Model;

namespace Generator
{
    public static class MethodExtension
    {
        public static string WriteNative(this Method? method, Namespace currentNamespace)
        {
            if (method is null )
                return string.Empty;

            if (method.Namespace is null)
                throw new Exception($"Method {method.Name} is missing a namespace");

            var returnValue = method.ReturnValue.WriteNative(currentNamespace);

            var summaryText = WriteNativeSummary(method);
            var dllImportText = $"[DllImport(\"{method.Namespace.Name}\", EntryPoint = \"{method.NativeName}\")]\r\n";
            var methodText = $"public static extern {returnValue} {method.ManagedName}({method.Arguments.WriteNative(currentNamespace)});\r\n";

            return summaryText + dllImportText + methodText;
        }
        
        public static string WriteNativeSummary(Method method)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"/// <summary>");
            builder.AppendLine($"/// Calls native method {method.NativeName}.");
            builder.AppendLine($"/// </summary>");

            foreach (var argument in method.Arguments)
            {
                builder.AppendLine($"/// <param name=\"{argument.NativeName}\">Transfer ownership: {argument.Transfer} Nullable: {argument.Nullable}</param>");
            }

            builder.AppendLine($"/// <returns>Transfer ownership: {method.ReturnValue.Transfer} Nullable: {method.ReturnValue.Nullable}</returns>");

            return builder.ToString();
        }
    }
}
