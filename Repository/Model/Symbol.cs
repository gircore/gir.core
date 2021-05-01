using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Symbol : SymbolReferenceProvider, Resolveable
    {
        public Namespace? Namespace { get; }
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

        public Symbol(string nativeName, string managedName)
            : this(new CTypeName(nativeName), new TypeName(nativeName), new SymbolName(managedName)) { }

        protected internal Symbol(CTypeName? ctypeName, TypeName typeName, SymbolName symbolName) : this(null, ctypeName, typeName, symbolName)
        {
        }

        protected internal Symbol(Namespace? @namespace, CTypeName? cTypeName, TypeName typeName, SymbolName symbolName)
        {
            Namespace = @namespace;
            CTypeName = cTypeName;
            TypeName = typeName;
            SymbolName = symbolName;
        }

        public virtual IEnumerable<SymbolReference> GetSymbolReferences()
            => Enumerable.Empty<SymbolReference>();

        public virtual bool GetIsResolved()
            => true;

        internal virtual void Strip() { }

        public override string ToString()
            => SymbolName;

        public string GetMetadataString(string key)
            => Metadata[key]?.ToString() ?? "";
    }
}
