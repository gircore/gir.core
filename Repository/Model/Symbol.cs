using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Metadata
    {
        private readonly Dictionary<string, object?> _metadata = new();

        public object? this[string key]
        {
            get => _metadata.TryGetValue(key, out var value) ? value : null;
            set => _metadata[key] = value;
        }

        public override string ToString()
            => string.Join(", ", _metadata.Keys);
    }

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
        /// Name of the symbol which should be used as a native representation
        /// </summary>
        public string NativeName { get; }
        
        /// <summary>
        /// Name of the symbol which should be used as managed representation
        /// </summary>
        public string ManagedName { get; set; }

        protected internal Symbol(Namespace? @namespace, string name, string managedName) : this(@namespace, name, name, managedName)
        {
        }
        
        protected internal Symbol(string name, string managedName) : this(name, name, managedName)
        {
        }
        
        protected internal Symbol(string name, string nativeName, string managedName) : this(null, name, nativeName, managedName)
        {
        }

        protected internal Symbol(Namespace? @namespace, string name, string nativeName, string managedName)
        {
            Namespace = @namespace;
            Name = name;
            NativeName = nativeName; 
            ManagedName = managedName;
        }

        public virtual IEnumerable<SymbolReference> GetSymbolReferences()
            => Enumerable.Empty<SymbolReference>();

        public override string ToString()
            => ManagedName;
    }
}
