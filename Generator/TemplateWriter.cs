using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository.Analysis;
using Repository.Model;

#nullable enable

namespace Generator
{
    public static class TemplateWriter
    {
        public static string WriteManagedArguments(IEnumerable<Argument> arguments)
        {
            var args = arguments.Select(x => WriteManagedSymbolReference(x.SymbolReference) + " " + x.Name);
            return string.Join(", ", args);
        }

        public static string WriteNativeArguments(IEnumerable<Argument> arguments)
        {
            var args = new List<string>();
            foreach (var argument in arguments)
            {
                var builder = new StringBuilder();

                builder.Append(argument.Direction switch
                {
                    Direction.OutCalleeAllocates => "out ",
                    Direction.OutCallerAllocates => "ref ",
                    _ => ""
                });
                
                builder.Append(WriteNativeSymbolReference(argument.SymbolReference));
                builder.Append(' ');
                builder.Append(argument.Name);

                args.Add(builder.ToString());
            }
            
            return string.Join(", ", args);
        }

        public static string WriteNativeSymbolReference(ISymbolReference symbolReference)
        {
            ISymbol symbol = symbolReference.GetSymbol();

            if (symbol is IType)
                return "IntPtr";

            if (symbolReference.IsArray)
                return InternalArray(symbol);

            return InternalType(symbol);
        }

        public static string WriteManagedSymbolReference(ISymbolReference symbolReference)
        {
            ISymbol symbol = symbolReference.GetSymbol();
            if (symbol is not IType type)
                return symbol.ManagedName;

            return symbolReference switch
            {
                { IsExternal: true, IsArray: true } => ExternalArray(type),
                { IsExternal: true, IsArray: false } => ExternalType(type),
                { IsExternal: false, IsArray: true } => InternalArray(type),
                { IsExternal: false, IsArray: false } => InternalType(type)
            };
        }

        private static string ExternalType(IType type)
            => $"{type.Namespace.Name}.{type.ManagedName}";

        private static string ExternalArray(IType type)
            => $"{type.Namespace.Name}.{type.ManagedName}[]";

        private static string InternalArray(ISymbol type)
            => $"{type.ManagedName}[]";

        private static string InternalType(ISymbol type)
            => type.ManagedName;

        public static string WriteInheritance(ISymbolReference? parent, IEnumerable<ISymbolReference> implements)
        {
            var builder = new StringBuilder();

            if (parent is { })
                builder.Append(": " + WriteManagedSymbolReference(parent));

            var refs = implements.ToList();
            if (refs.Count == 0)
                return builder.ToString();

            if (parent is { })
                builder.Append(", ");

            builder.Append(string.Join(", ", refs.Select(WriteManagedSymbolReference)));
            return builder.ToString();
        }

        public static string WriteNativeMethod(Method method)
        {
            var returnValue = WriteNativeSymbolReference(method.ReturnValue.SymbolReference);

            var summaryText = WriteNativeSummary(method);
            var dllImportText = $"[DllImport(\"{method.Namespace.Name}\", EntryPoint = \"{method.NativeName}\")]\r\n";
            var methodText = $"public static extern {returnValue} {method.ManagedName}({WriteNativeArguments(method.Arguments)});\r\n";
            
            return summaryText + dllImportText + methodText;
        }

        public static string WriteNativeSummary(Method method)
        {
            var builder = new StringBuilder();

            foreach (var argument in method.Arguments)
            {
                builder.Append($"/// <param name=\"{argument.Name}\">Transfer ownership: {argument.Transfer} Nullable: {argument.Nullable}</param>\r\n");
            }

            builder.Append($"/// <returns>Transfer ownership: {method.ReturnValue.Transfer} Nullable: {method.ReturnValue.Nullable}</returns>\r\n");
            
            return builder.ToString();
        }

        public static string GetIf(string text, bool condition)
            => condition ? text : "";
    }
}
