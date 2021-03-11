using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public interface  ISymbolReferenceProvider
    {
        IEnumerable<SymbolReference> GetSymbolReferences();
    }

    public class Symbol : ISymbolReferenceProvider
    {
        public Namespace? Namespace { get; }
        public Metadata Metadata { get; } = new();
        
        /// <summary>
        /// Original name of the symbol.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Name of the symbol in the c world
        /// </summary>
        public string? CName { get; }
        
        /// <summary>
        /// Name of the symbol which should be used as a native representation
        /// </summary>
        public string NativeName { get; set; }
        
        /// <summary>
        /// Name of the symbol which should be used as managed representation
        /// </summary>
        public string ManagedName { get; set; }

        protected internal Symbol(string cname, string name, string nativeName, string managedName): this(null, cname, name, nativeName, managedName)
        {
        }

        protected internal Symbol(Namespace? @namespace, string? cname, string name, string nativeName, string managedName)
        {
            Namespace = @namespace;
            CName = cname;
            Name = name;
            NativeName = nativeName; 
            ManagedName = managedName;
        }

        public virtual IEnumerable<SymbolReference> GetSymbolReferences()
            => Enumerable.Empty<SymbolReference>();

        public override string ToString()
            => ManagedName;

        public static Symbol Primitive(string nativeName, string managedName)
            => new Symbol(nativeName, nativeName, managedName, managedName);
    }
}
