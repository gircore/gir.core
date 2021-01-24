using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class TypeDictionary
    {
        // Maps Symbol Name -> Introspection Data
        // Namespace-specific
        private class SymbolDictionary
        {
            private readonly Dictionary<string, ISymbol> _symbolDict = new();
            private string Namespace { get; }

            public SymbolDictionary(string nspace)
            {
                Namespace = nspace;
            }

            public void AddSymbol(string name, ISymbol info)
                => _symbolDict.Add(name, info);

            public ISymbol GetSymbol(string name)
                => _symbolDict[name];

            public bool TryGetSymbol(string name, [NotNullWhen(true)] out ISymbol symbol)
                => _symbolDict.TryGetValue(name, out symbol);
        }
    }
}
