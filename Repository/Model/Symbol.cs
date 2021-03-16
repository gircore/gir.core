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
        public NativeName NativeName { get; set; }
        
        /// <summary>
        /// Name of the symbol which should be used as managed representation
        /// </summary>
        public ManagedName ManagedName { get; set; }

        //TODO: Verify if one of nativename / managedname can be removed completly
        
        protected internal Symbol(CTypeName? ctypeName, TypeName typeName, NativeName nativeName, ManagedName managedName): this(null, ctypeName, typeName, nativeName, managedName)
        {
        }

        protected internal Symbol(Namespace? @namespace, CTypeName? cTypeName, TypeName typeName, NativeName nativeName, ManagedName managedName)
        {
            Namespace = @namespace;
            CTypeName = cTypeName;
            TypeName = typeName;
            NativeName = nativeName; 
            ManagedName = managedName;
        }

        public virtual IEnumerable<SymbolReference> GetSymbolReferences()
            => Enumerable.Empty<SymbolReference>();

        public virtual bool GetIsResolved()
            => true;

        internal virtual void Strip() {}
        
        public override string ToString()
            => ManagedName;

        public static Symbol Primitive(string nativeName, string managedName)
            => new Symbol(
                ctypeName: new CTypeName(nativeName),
                typeName: new TypeName(nativeName), 
                nativeName: new NativeName(managedName), 
                managedName: new ManagedName(managedName)
            );
    }
}
