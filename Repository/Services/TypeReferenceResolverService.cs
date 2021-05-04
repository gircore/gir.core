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
            AddFundamentalTypes(symbolDictionary);

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
            if (symbolDictionary.TryLookup(reference, out var symbol))
                reference.ResolveAs(symbol);
        }

        private static void AddFundamentalTypes(SymbolDictionary symbolDictionary)
        {
            symbolDictionary.AddSymbol(new Void("none"));
            symbolDictionary.AddSymbol(new Void("void"));

            symbolDictionary.AddSymbol(new Boolean("gboolean"));
            
            symbolDictionary.AddSymbol(new Float("gfloat"));
            symbolDictionary.AddSymbol(new Float("float"));

            symbolDictionary.AddSymbol(new IntPtr("any"));
            symbolDictionary.AddSymbol(new IntPtr("gconstpointer"));
            symbolDictionary.AddSymbol(new IntPtr("va_list"));
            symbolDictionary.AddSymbol(new IntPtr("gpointer"));
            symbolDictionary.AddSymbol(new IntPtr("tm"));

            // TODO: Should we use UIntPtr here? Non-CLR compliant
            symbolDictionary.AddSymbol(new UnsignedIntPtr("guintptr"));

            symbolDictionary.AddSymbol(new UnsignedShort("guint16"));
            symbolDictionary.AddSymbol(new UnsignedShort("gushort"));

            symbolDictionary.AddSymbol(new Short("gint16"));
            symbolDictionary.AddSymbol(new Short("gshort"));

            symbolDictionary.AddSymbol(new Double("double"));
            symbolDictionary.AddSymbol(new Double("gdouble"));
            symbolDictionary.AddSymbol(new Double("long double"));

            symbolDictionary.AddSymbol(new Integer("int"));
            symbolDictionary.AddSymbol(new Integer("gint"));
            symbolDictionary.AddSymbol(new Integer("gint32"));
            symbolDictionary.AddSymbol(new Integer("pid_t"));

            symbolDictionary.AddSymbol(new UnsignedInteger("unsigned int"));
            symbolDictionary.AddSymbol(new UnsignedInteger("unsigned"));
            symbolDictionary.AddSymbol(new UnsignedInteger("guint"));
            symbolDictionary.AddSymbol(new UnsignedInteger("guint32"));
            symbolDictionary.AddSymbol(new UnsignedInteger("gunichar"));
            symbolDictionary.AddSymbol(new UnsignedInteger("uid_t"));

            symbolDictionary.AddSymbol(new Byte("guchar"));
            symbolDictionary.AddSymbol(new Byte("guint8"));

            symbolDictionary.AddSymbol(new SignedByte("gchar"));
            symbolDictionary.AddSymbol(new SignedByte("char"));
            symbolDictionary.AddSymbol(new SignedByte("gint8"));

            symbolDictionary.AddSymbol(new Long("glong"));
            symbolDictionary.AddSymbol(new Long("gssize"));
            symbolDictionary.AddSymbol(new Long("gint64"));
            symbolDictionary.AddSymbol(new Long("goffset"));
            symbolDictionary.AddSymbol(new Long("time_t"));

            symbolDictionary.AddSymbol(new NativeUnsignedInteger("gsize"));
            symbolDictionary.AddSymbol(new UnsignedLong("guint64"));
            symbolDictionary.AddSymbol(new UnsignedLong("gulong"));

            symbolDictionary.AddSymbol(new Utf8String());
            symbolDictionary.AddSymbol(new PlatformString());
        }
    }
}
