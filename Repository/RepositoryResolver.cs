using Repository.Analysis;
using Repository.Model;

namespace Repository
{
    internal class RepositoryResolver
    {
        private TypeDictionary TypeDictionary { get; } = new();
        
        public RepositoryResolver()
        {
            AddFundamentalTypes();
        }
        
        private void AddFundamentalTypes()
        {
            TypeDictionary.AddType(new Void("none"));
            TypeDictionary.AddType(new Void("void"));

            TypeDictionary.AddType(new Boolean("gboolean"));

            TypeDictionary.AddType(new Float("gfloat"));
            TypeDictionary.AddType(new Float("float"));

            TypeDictionary.AddType(new Pointer("any"));
            TypeDictionary.AddType(new Pointer("gconstpointer"));
            TypeDictionary.AddType(new Pointer("va_list"));
            TypeDictionary.AddType(new Pointer("gpointer"));
            TypeDictionary.AddType(new Pointer("tm"));

            // TODO: Should we use UIntPtr here? Non-CLR compliant
            TypeDictionary.AddType(new UnsignedPointer("guintptr"));

            TypeDictionary.AddType(new UnsignedShort("guint16"));
            TypeDictionary.AddType(new UnsignedShort("gushort"));

            TypeDictionary.AddType(new Short("gint16"));
            TypeDictionary.AddType(new Short("gshort"));

            TypeDictionary.AddType(new Double("double"));
            TypeDictionary.AddType(new Double("gdouble"));
            TypeDictionary.AddType(new Double("long double"));

            TypeDictionary.AddType(new Integer("int"));
            TypeDictionary.AddType(new Integer("gint"));
            TypeDictionary.AddType(new Integer("gint32"));
            TypeDictionary.AddType(new Integer("pid_t"));

            TypeDictionary.AddType(new UnsignedInteger("unsigned int"));
            TypeDictionary.AddType(new UnsignedInteger("unsigned"));
            TypeDictionary.AddType(new UnsignedInteger("guint"));
            TypeDictionary.AddType(new UnsignedInteger("guint32"));
            TypeDictionary.AddType(new UnsignedInteger("gunichar"));
            TypeDictionary.AddType(new UnsignedInteger("uid_t"));

            TypeDictionary.AddType(new Byte("guchar"));
            TypeDictionary.AddType(new Byte("guint8"));

            TypeDictionary.AddType(new SignedByte("gchar"));
            TypeDictionary.AddType(new SignedByte("char"));
            TypeDictionary.AddType(new SignedByte("gint8"));

            TypeDictionary.AddType(new Long("glong"));
            TypeDictionary.AddType(new Long("gssize"));
            TypeDictionary.AddType(new Long("gint64"));
            TypeDictionary.AddType(new Long("goffset"));
            TypeDictionary.AddType(new Long("time_t"));

            TypeDictionary.AddType(new NativeUnsignedInteger("gsize"));
            TypeDictionary.AddType(new UnsignedLong("guint64"));
            TypeDictionary.AddType(new UnsignedLong("gulong"));

            TypeDictionary.AddType(new Utf8String());
            TypeDictionary.AddType(new PlatformString());
        }
        
        /// <summary>
        /// It is important to resolve repositories from the most basic one
        /// to the most derived one.
        /// </summary>
        public void Resolve(Model.Repository repository)
        {
            FillTypeDictionary(repository.Namespace);
            TypeDictionary.ResolveAliases(repository.Namespace.Aliases);
            ResolveTypeReferences(repository.Namespace);
        }
        
        private void FillTypeDictionary(Namespace @namespace)
        {
            TypeDictionary.AddTypes(@namespace.Classes);
            TypeDictionary.AddTypes(@namespace.Interfaces);
            TypeDictionary.AddTypes(@namespace.Callbacks);
            TypeDictionary.AddTypes(@namespace.Enumerations);
            TypeDictionary.AddTypes(@namespace.Bitfields);
            TypeDictionary.AddTypes(@namespace.Records);
            TypeDictionary.AddTypes(@namespace.Unions);
        }

        private void ResolveTypeReferences(Namespace ns)
        {
            foreach (var reference in ns.GetTypeReferences())
                ResolveTypeReference(reference);
        }
        
        private void ResolveTypeReference(TypeReference reference)
        {
            if (TypeDictionary.TryLookup(reference, out var type))
                reference.ResolveAs(type);
        }
    }
}
