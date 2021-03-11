using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Repository.Model;

namespace Repository.Analysis
{
    internal partial class SymbolDictionary
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

            public bool GetSymbol(string name, [MaybeNullWhen(false)] out Symbol symbol)
            {
                return _typeDict.TryGetValue(name, out symbol);
            }

            public bool TryGetSymbol(string name, [NotNullWhen(true)] out Symbol? type)
                => _typeDict.TryGetValue(name, out type);
        }
    }
}
