using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class SymbolDictionary
    {
        private class SymbolCache
        {
            private readonly HashSet<Symbol> _symbols = new();

            public void Add(Symbol symbol)
            {
                _symbols.Add(symbol);
            }

            public bool TryLookup(SymbolReference symbolReference, [MaybeNullWhen(false)] out Symbol symbol)
            {
                symbol = _symbols.FirstOrDefault(x => Check(x, symbolReference));

                return symbol is not null;
            }

            private bool Check(Symbol symbol, SymbolReference symbolReference)
            {
                if (!CheckNamespace(symbol, symbolReference))
                    return false;

                return (symbol.TypeName == symbolReference.TypeName); //Check cname, too?|| (symbol.CName == symbolReference.CTypeName);
            }

            private static bool CheckNamespace(Symbol symbol, SymbolReference symbolReference) => (symbol, symbolReference) switch
            {
                ({Namespace: {} }, {NamespaceName: null}) => false,
                ({Namespace: {Name: {}n1}}, {NamespaceName:{}n2}) when n1 != n2 => false,
                _ => true
            };
        }
    }
}
