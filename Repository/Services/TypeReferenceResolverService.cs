using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Model;

namespace Repository.Services
{
    internal class TypeReferenceResolverService 
    {
        public static void Resolve(IEnumerable<Namespace> namespaces)
        {
            var symbolDictionary = new SymbolDictionary();

            var namespaceList = namespaces.ToList();
            foreach (var ns in namespaceList)
            {
                Log.Debug($"Analysing '{ns.Name}'.");
                FillSymbolDictionary(symbolDictionary, ns);
                
                symbolDictionary.ResolveAliases(ns.Aliases);

                ResolveReferences(symbolDictionary, ns);
                Log.Information($"Resolved symbol references for {ns.Name}.");
            }
        }

        private static void FillSymbolDictionary(SymbolDictionary symbolDictionary, Namespace @namespace)
        {
            AddPrimitives(symbolDictionary);

            symbolDictionary.AddSymbols(@namespace.Classes);
            symbolDictionary.AddSymbols(@namespace.Interfaces);
            symbolDictionary.AddSymbols(@namespace.Callbacks);
            symbolDictionary.AddSymbols(@namespace.Enumerations);
            symbolDictionary.AddSymbols(@namespace.Bitfields);
            symbolDictionary.AddSymbols(@namespace.Records);
            symbolDictionary.AddSymbols(@namespace.Unions);
        }

        private static void ResolveReferences(SymbolDictionary symbolDictionary, Namespace ns)
        {
            foreach (var reference in ns.GetSymbolReferences())
            {
                Resolve(symbolDictionary, reference);
            }
        }

        private static void Resolve(SymbolDictionary symbolDictionary, SymbolReference reference)
        {
            if(symbolDictionary.TryLookup(reference, out var symbol))
                reference.ResolveAs(symbol);
        }

        private static void AddPrimitives(SymbolDictionary symbolDictionary)
        {
             // Add Fundamental Types
            // Fundamental types are accessible regardless of namespace and
            // take priority over any namespaced variant.

            symbolDictionary.AddSymbol(Symbol.Primitive("none", "void"));
            symbolDictionary.AddSymbol(Symbol.Primitive("any", "IntPtr"));

            symbolDictionary.AddSymbol(Symbol.Primitive("void", "void"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gboolean", "bool"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gfloat", "float"));
            symbolDictionary.AddSymbol(Symbol.Primitive("float", "float"));

            symbolDictionary.AddSymbol(Symbol.Primitive("gconstpointer", "IntPtr"));
            symbolDictionary.AddSymbol(Symbol.Primitive("va_list", "IntPtr"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gpointer", "IntPtr"));
            symbolDictionary.AddSymbol(Symbol.Primitive("tm", "IntPtr"));
            
            // TODO: Should we use UIntPtr here? Non-CLR compliant
            symbolDictionary.AddSymbol(Symbol.Primitive("guintptr", "UIntPtr"));

            symbolDictionary.AddSymbol(Symbol.Primitive("guint16", "ushort"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gushort", "ushort"));

            symbolDictionary.AddSymbol(Symbol.Primitive("gint16", "short"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gshort", "short"));

            symbolDictionary.AddSymbol(Symbol.Primitive("double", "double"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gdouble", "double"));
            symbolDictionary.AddSymbol(Symbol.Primitive("long double", "double"));

            // AddBasicSymbol(new BasicSymbol("cairo_format_t", "int"));
            symbolDictionary.AddSymbol(Symbol.Primitive("int", "int"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gint", "int"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gint32", "int"));
            symbolDictionary.AddSymbol(Symbol.Primitive("pid_t", "int"));

            symbolDictionary.AddSymbol(Symbol.Primitive("unsigned int", "uint"));
            symbolDictionary.AddSymbol(Symbol.Primitive("unsigned", "uint"));
            symbolDictionary.AddSymbol(Symbol.Primitive("guint", "uint"));
            symbolDictionary.AddSymbol(Symbol.Primitive("guint32", "uint"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gunichar", "uint"));
            symbolDictionary.AddSymbol(Symbol.Primitive("uid_t", "uint"));
            // AddBasicSymbol(new BasicSymbol("GQuark", "uint"));

            symbolDictionary.AddSymbol(Symbol.Primitive("guchar", "byte"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gchar", "sbyte"));
            symbolDictionary.AddSymbol(Symbol.Primitive("char", "sbyte"));
            symbolDictionary.AddSymbol(Symbol.Primitive("guint8", "byte"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gint8", "sbyte"));

            symbolDictionary.AddSymbol(Symbol.Primitive("glong", "long"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gssize", "long"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gint64", "long"));
            symbolDictionary.AddSymbol(Symbol.Primitive("goffset", "long"));
            symbolDictionary.AddSymbol(Symbol.Primitive("time_t", "long"));

            symbolDictionary.AddSymbol(Symbol.Primitive("gsize", "ulong"));
            symbolDictionary.AddSymbol(Symbol.Primitive("guint64", "ulong"));
            symbolDictionary.AddSymbol(Symbol.Primitive("gulong", "ulong"));

            symbolDictionary.AddSymbol(Symbol.Primitive("utf8", "string"));
            symbolDictionary.AddSymbol(Symbol.Primitive("filename", "string"));
            // AddBasicSymbol(new BasicSymbol("Window", "ulong"));
        }
    }
}
