using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static string WriteCallbackMarshaller(IEnumerable<Argument> arguments, ReturnValue returnValue, Namespace currentNamespace)
        {
            var builder = new StringBuilder();
            var args = new List<string>();

            foreach (Argument arg in arguments)
            {
                // Skip 'user_data' parameters (for callbacks, when closure index is not zero)
                if (arg.ClosureIndex.HasValue)
                    continue;

                builder.AppendLine(WriteMarshalArgumentToManaged(arg, currentNamespace));
                args.Add(arg.ManagedName + "Managed");
            }

            var funcArgs = string.Join(
                separator: ", ", 
                values: args
            );
            
            var funcCall = returnValue.IsVoid()
                ? $"managedCallback({funcArgs});" 
                : $"var managed_callback_result = managedCallback({funcArgs});";

            builder.Append(funcCall);

            return builder.ToString();
        }

        private static string WriteMarshalArgumentToManaged(Argument arg, Namespace currentNamespace)
        {
            // TODO: We need to support disguised structs (opaque types)
            Symbol symbol = arg.SymbolReference.GetSymbol();
            var managedType = arg.GetType(Target.Managed, currentNamespace);
            
            
            var expression = symbol switch
            {
                // GObject -> Use Object.WrapHandle
                Class => $"Object.WrapHandle<{managedType}>({arg.NativeName});",

                // Struct -> Use struct marshalling (TODO: Should support opaque types)
                Record => $"Marshal.PtrToStructure<{managedType}>({arg.NativeName});",

                // Other -> Try a brute-force cast
                _ => $"({managedType}){arg.NativeName};"
            };

            return $"{arg.WriteTypeAndName(Target.Managed, currentNamespace)}Managed = " + expression;
        }

        public static bool SignalsHaveArgs(IEnumerable<Signal> signals)
            => signals.Any(x => x.Arguments.Any());
    }
}
