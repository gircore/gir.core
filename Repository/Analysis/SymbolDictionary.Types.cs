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
            private readonly Dictionary<string, Symbol> _typeDict = new();
            private string? Namespace { get; }

            public TypeDictionary(string? nspace)
            {
                Namespace = nspace;
            }

            public void AddSymbol(string name, Symbol info)
                => _typeDict.Add(name, info);

            public Symbol GetSymbol(string name)
                => _typeDict[name];

            public bool TryGetSymbol(string name, [NotNullWhen(true)] out Symbol? type)
                => _typeDict.TryGetValue(name, out type);
        }
    }
}
