using System;
using System.Linq;
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

            var summaryText = WriteNativeSummary(method);
            var dllImportText = $"[DllImport(\"{currentNamespace.Name}\", EntryPoint = \"{method.Name}\")]\r\n";
            var methodText = $"public static extern {returnValue} {method.ManagedName}({method.Arguments.WriteNative(currentNamespace)});\r\n";

            return summaryText + dllImportText + methodText;
        }
        
        public static string WriteManaged(this Method? method, Namespace currentNamespace)
        {
            if (method is null)
                return string.Empty;
            
            var builder = new StringBuilder();
            
            var delegateParams = method.Arguments.Where(arg => arg.SymbolReference.GetSymbol().GetType() == typeof(Callback));
            var marshalParams = method.Arguments.Except(delegateParams);
            var returnValue = method.ReturnValue;

            builder.AppendLine("// Method: " + method.ManagedName);

            foreach (var arg in delegateParams)
                builder.AppendLine("// Delegate Arg: " + arg.ManagedName);
            
            foreach (var arg in marshalParams)
                builder.AppendLine("// Marshal Arg: " + arg.ManagedName);

            builder.AppendLine("// With Return Value: " + returnValue.WriteManaged(currentNamespace));
            builder.Append("\n\n\n");

            return builder.ToString();
        }
        
        public static string WriteNativeSummary(Method method)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"/// <summary>");
            builder.AppendLine($"/// Calls native method {method.Name}.");
            builder.AppendLine($"/// </summary>");

            foreach (var argument in method.Arguments)
            {
                builder.AppendLine($"/// <param name=\"{argument.ManagedName}\">Transfer ownership: {argument.Transfer} Nullable: {argument.Nullable}</param>");
            }

            builder.AppendLine($"/// <returns>Transfer ownership: {method.ReturnValue.Transfer} Nullable: {method.ReturnValue.Nullable}</returns>");

            return builder.ToString();
        }
    }
}
