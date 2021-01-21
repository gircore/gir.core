using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Repository.Xml.Analysis
{
    public partial class TypeDictionary
    {
        // Maps Symbol Name -> Introspection Data
        // Namespace-specific
        private class SymbolDictionary
        {
            private readonly Dictionary<string, ISymbolInfo> _symbolDict = new();
            private string Namespace { get; }

            public SymbolDictionary(string nspace)
            {
                Namespace = nspace;
            }

            public void AddSymbol(string name, ISymbolInfo info)
                => _symbolDict.Add(name, info);

            public ISymbolInfo GetSymbol(string name)
                => _symbolDict[name];

            public bool TryGetSymbol(string name, [NotNullWhen(true)] out ISymbolInfo symbol)
                => _symbolDict.TryGetValue(name, out symbol);
        }
    }
}
