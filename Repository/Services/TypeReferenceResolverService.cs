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
            var symbolDictionary = new TypeDictionary();

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

        private static void FillSymbolDictionary(TypeDictionary typeDictionary, Namespace @namespace)
        {
            AddFundamentalTypes(typeDictionary);

            typeDictionary.AddTypes(@namespace.Classes);
            typeDictionary.AddTypes(@namespace.Interfaces);
            typeDictionary.AddTypes(@namespace.Callbacks);
            typeDictionary.AddTypes(@namespace.Enumerations);
            typeDictionary.AddTypes(@namespace.Bitfields);
            typeDictionary.AddTypes(@namespace.Records);
            typeDictionary.AddTypes(@namespace.Unions);
        }

        private static void ResolveReferences(TypeDictionary typeDictionary, Namespace ns)
        {
            foreach (var reference in ns.GetTypeReferences())
            {
                Resolve(typeDictionary, reference);
            }
        }

        private static void Resolve(TypeDictionary typeDictionary, TypeReference reference)
        {
            if (typeDictionary.TryLookup(reference, out var symbol))
                reference.ResolveAs(symbol);
        }

        private static void AddFundamentalTypes(TypeDictionary typeDictionary)
        {
            typeDictionary.AddType(new Void("none"));
            typeDictionary.AddType(new Void("void"));

            typeDictionary.AddType(new Boolean("gboolean"));

            typeDictionary.AddType(new Float("gfloat"));
            typeDictionary.AddType(new Float("float"));

            typeDictionary.AddType(new Pointer("any"));
            typeDictionary.AddType(new Pointer("gconstpointer"));
            typeDictionary.AddType(new Pointer("va_list"));
            typeDictionary.AddType(new Pointer("gpointer"));
            typeDictionary.AddType(new Pointer("tm"));

            // TODO: Should we use UIntPtr here? Non-CLR compliant
            typeDictionary.AddType(new UnsignedPointer("guintptr"));

            typeDictionary.AddType(new UnsignedShort("guint16"));
            typeDictionary.AddType(new UnsignedShort("gushort"));

            typeDictionary.AddType(new Short("gint16"));
            typeDictionary.AddType(new Short("gshort"));

            typeDictionary.AddType(new Double("double"));
            typeDictionary.AddType(new Double("gdouble"));
            typeDictionary.AddType(new Double("long double"));

            typeDictionary.AddType(new Integer("int"));
            typeDictionary.AddType(new Integer("gint"));
            typeDictionary.AddType(new Integer("gint32"));
            typeDictionary.AddType(new Integer("pid_t"));

            typeDictionary.AddType(new UnsignedInteger("unsigned int"));
            typeDictionary.AddType(new UnsignedInteger("unsigned"));
            typeDictionary.AddType(new UnsignedInteger("guint"));
            typeDictionary.AddType(new UnsignedInteger("guint32"));
            typeDictionary.AddType(new UnsignedInteger("gunichar"));
            typeDictionary.AddType(new UnsignedInteger("uid_t"));

            typeDictionary.AddType(new Byte("guchar"));
            typeDictionary.AddType(new Byte("guint8"));

            typeDictionary.AddType(new SignedByte("gchar"));
            typeDictionary.AddType(new SignedByte("char"));
            typeDictionary.AddType(new SignedByte("gint8"));

            typeDictionary.AddType(new Long("glong"));
            typeDictionary.AddType(new Long("gssize"));
            typeDictionary.AddType(new Long("gint64"));
            typeDictionary.AddType(new Long("goffset"));
            typeDictionary.AddType(new Long("time_t"));

            typeDictionary.AddType(new NativeUnsignedInteger("gsize"));
            typeDictionary.AddType(new UnsignedLong("guint64"));
            typeDictionary.AddType(new UnsignedLong("gulong"));

            typeDictionary.AddType(new Utf8String());
            typeDictionary.AddType(new PlatformString());
        }
    }
}
