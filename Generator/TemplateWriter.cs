using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository;
using Repository.Analysis;
using Repository.Model;

namespace Generator
{
    internal static class TemplateWriter
    {
        public static string WriteInheritance(SymbolReference? parent, IEnumerable<SymbolReference> implements)
        {
            var builder = new StringBuilder();

            if (parent is { })
                builder.Append(": " + parent.GetSymbol().ManagedName);

            var refs = implements.ToList();
            if (refs.Count == 0)
                return builder.ToString();

            if (parent is { })
                builder.Append(", ");

            builder.Append(string.Join(", ", refs.Select(x => x.GetSymbol().ManagedName)));
            return builder.ToString();
        }

        public static string GetIf(string text, bool condition)
            => condition ? text : "";

        public static string WriteCallbackMarshaller(IEnumerable<Argument> arguments, string funcName, bool hasReturnValue)
        {
            var builder = new StringBuilder();
            var args = new List<string>();

            foreach (Argument arg in arguments)
            {
                // Skip 'user_data' parameters (for callbacks, when closure index is not zero)
                if (arg.ClosureIndex != 0)
                    continue;

                var newName = arg.ManagedName + "Parameter";
                builder.AppendLine(WriteMarshalArgumentToManaged(arg, newName));
                args.Add(newName);
            }

            var funcArgs = string.Join(separator: ", ", values: args);
            var funcCall = hasReturnValue
                ? $"var result = {funcName}({funcArgs});"
                : $"{funcName}({funcArgs});";

            builder.Append(funcCall);

            return builder.ToString();
        }

        public static string WriteMarshalArgumentToManaged(Argument arg, string paramName)
        {
            // TODO: We need to support disguised structs (opaque types)
            Symbol symbol = arg.SymbolReference.GetSymbol();
            var fromName = arg.ManagedName;
            var managedType = symbol.ManagedName;

            var expression = symbol switch
            {
                // GObject -> Use Object.WrapHandle
                Class => $"Object.WrapHandle<{managedType}>({fromName});",

                // Struct -> Use struct marshalling (TODO: Should support opaque types)
                Record => $"Marshal.PtrToStructure<{managedType}>({fromName});",

                // Other -> Try a brute-force cast
                _ => $"({managedType}){fromName};"
            };

            return $"{managedType} {paramName} = " + expression;
        }

        public static bool SignalsHaveArgs(IEnumerable<Signal> signals)
            => signals.Any(x => x.Arguments.Any());
    }
}
