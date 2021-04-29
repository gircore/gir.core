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

            symbolDictionary.AddSymbol(new Symbol("none", "void"));
            symbolDictionary.AddSymbol(new Symbol("any", "IntPtr"));

            symbolDictionary.AddSymbol(new Symbol("void", "void"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gboolean", "bool"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gfloat", "float"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("float", "float"));

            symbolDictionary.AddSymbol(new Symbol("gconstpointer", "IntPtr"));
            symbolDictionary.AddSymbol(new Symbol("va_list", "IntPtr"));
            symbolDictionary.AddSymbol(new Symbol("gpointer", "IntPtr"));
            symbolDictionary.AddSymbol(new Symbol("tm", "IntPtr"));
            
            // TODO: Should we use UIntPtr here? Non-CLR compliant
            symbolDictionary.AddSymbol(new Symbol("guintptr", "UIntPtr"));

            symbolDictionary.AddSymbol(new PrimitiveValueType("guint16", "ushort"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gushort", "ushort"));

            symbolDictionary.AddSymbol(new PrimitiveValueType("gint16", "short"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gshort", "short"));

            symbolDictionary.AddSymbol(new PrimitiveValueType("double", "double"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gdouble", "double"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("long double", "double"));

            symbolDictionary.AddSymbol(new PrimitiveValueType("int", "int"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gint", "int"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gint32", "int"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("pid_t", "int"));

            symbolDictionary.AddSymbol(new PrimitiveValueType("unsigned int", "uint"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("unsigned", "uint"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("guint", "uint"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("guint32", "uint"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gunichar", "uint"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("uid_t", "uint"));

            symbolDictionary.AddSymbol(new PrimitiveValueType("guchar", "byte"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gchar", "sbyte"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("char", "sbyte"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("guint8", "byte"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gint8", "sbyte"));

            symbolDictionary.AddSymbol(new PrimitiveValueType("glong", "long"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gssize", "long"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gint64", "long"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("goffset", "long"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("time_t", "long"));

            symbolDictionary.AddSymbol(new PrimitiveValueType("gsize", "nuint"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("guint64", "ulong"));
            symbolDictionary.AddSymbol(new PrimitiveValueType("gulong", "ulong"));

            symbolDictionary.AddSymbol(new Utf8String());
            symbolDictionary.AddSymbol(new PlatformString());
        }
    }
}
