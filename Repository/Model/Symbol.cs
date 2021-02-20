using System.Collections.Generic;
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

    public abstract class Symbol : ISymbolReferenceProvider
    {
        public Metadata Metadata { get; } = new();
        public string NativeName { get; }
        public string ManagedName { get; set; }

        public Symbol(string nativeName, string managedName)
        {
            NativeName = nativeName;
            ManagedName = managedName;
        }

        public abstract IEnumerable<SymbolReference> GetSymbolReferences();

        public override string ToString()
            => ManagedName;
    }
}
