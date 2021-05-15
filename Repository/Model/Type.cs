using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
{
    public abstract class Type : TypeReferenceProvider, Resolveable
    {
        public Repository? Repository { get; }
        public Metadata Metadata { get; } = new();

        /// <summary>
        /// Original name of the symbol.
        /// </summary>
        public TypeName TypeName { get; }

        /// <summary>
        /// Name of the symbol in the c world
        /// </summary>
        public CTypeName? CTypeName { get; }

        /// <summary>
        /// Name of the symbol which should be used as a native representation
        /// </summary>
        public SymbolName SymbolName { get; set; }

        public Type(string nativeName, string managedName)
            : this(new CTypeName(nativeName), new TypeName(nativeName), new SymbolName(managedName)) { }

        protected internal Type(CTypeName? ctypeName, TypeName typeName, SymbolName symbolName) : this(null, ctypeName, typeName, symbolName)
        {
        }

        protected internal Type(Repository? repository, CTypeName? cTypeName, TypeName typeName, SymbolName symbolName)
        {
            Repository = repository;
            CTypeName = cTypeName;
            TypeName = typeName;
            SymbolName = symbolName;
        }

        public virtual IEnumerable<TypeReference> GetTypeReferences()
            => Enumerable.Empty<TypeReference>();

        public virtual bool GetIsResolved()
            => true;

        internal virtual void Strip() { }

        public override string ToString()
            => SymbolName;

        public string GetMetadataString(string key)
            => Metadata[key]?.ToString() ?? "";
    }
}
