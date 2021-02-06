using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class SymbolDictionary
    {
        // Maps Symbol Name -> Introspection Data
        // Namespace-specific
        private class TypeDictionary
        {
            private readonly Dictionary<string, ISymbol> _typeDict = new();
            private string? Namespace { get; }

            public TypeDictionary(string? nspace)
            {
                Namespace = nspace;
            }

            public void AddSymbol(string name, ISymbol info)
                => _typeDict.Add(name, info);

            public ISymbol GetSymbol(string name)
                => _typeDict[name];

            public bool TryGetSymbol(string name, [NotNullWhen(true)] out ISymbol? type)
                => _typeDict.TryGetValue(name, out type);
        }
    }
}
