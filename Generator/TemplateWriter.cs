using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository.Analysis;
using Repository.Model;

namespace Generator
{
    internal static class TemplateWriter
    {
        public static string WriteClassInheritance(SymbolReference? parent, IEnumerable<SymbolReference> implements, Namespace currentNamespace)
        {
            var builder = new StringBuilder();
            if (parent is { })
                builder.Append(": " + parent.GetSymbol().Write(Target.Managed, currentNamespace));
            
            var refs = implements.ToList();
            if (refs.Count == 0)
                return builder.ToString();

            builder.Append(parent is { } ? ", " : ": ");
            builder.Append(string.Join(", ", refs.Select(x => x.GetSymbol().Write(Target.Managed, currentNamespace))));
            return builder.ToString();
        }
        
        public static string WriteInterfaceInheritance(IEnumerable<SymbolReference> implements, Namespace currentNamespace)
        {
            StringBuilder builder = new();
            builder.Append(": GLib.IHandle");

            var implementString = string.Join(
                separator: ",",
                values: implements.Select(x => x.Symbol?.Write(Target.Managed, currentNamespace))
            );

            if (!string.IsNullOrWhiteSpace(implementString))
                builder.Append(", " + implementString);

            return builder.ToString();
        }

        public static string WriteNativeParent(SymbolReference? parent, Namespace currentNamespace)
        {
            if (parent is { })
                return ": " + parent.GetSymbol().Write(Target.Managed, currentNamespace) + ".Native";

            return "";
        }

        public static string GetIf(string text, bool condition)
            => condition ? text : "";
    }
}
