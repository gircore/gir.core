using System;
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
            var builder = new StringBuilder();

            foreach (Argument argument in arguments)
            {
            }

            return builder.ToString();
        }

        public static string WriteNativeSymbolReference(ISymbolReference symbolReference)
        {
            if (symbolReference.Symbol is null)
                throw new InvalidOperationException($"The symbolreference for {symbolReference.Name} Reference has not been resolved. It cannot be printed.");

            if (symbolReference.Symbol is IType)
                return "IntPtr";

            return symbolReference.Symbol.ManagedName;
        }

        public static string WriteManagedSymbolReference(ISymbolReference symbolReference)
        {
            if (symbolReference.Symbol is null)
                throw new InvalidOperationException($"The Type for {symbolReference.Name} Reference has not been resolved. It cannot be printed.");

            if (symbolReference.Symbol is not IType type)
                return symbolReference.Symbol.ManagedName;

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

        private static string InternalArray(IType type)
            => $"{type.ManagedName}[]";

        private static string InternalType(IType type)
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
            return $"public static extern {returnValue} {method.Name}({WriteManagedArguments(method.Arguments)});\r\n";
        }
    }
}
