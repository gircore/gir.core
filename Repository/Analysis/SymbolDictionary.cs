using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class SymbolDictionary
    {
        private readonly Dictionary<NamespaceName, SymbolCache> _data = new();
        private readonly SymbolCache _globalSymbols = new();

        public bool TryLookup(SymbolReference symbolReference, [MaybeNullWhen(false)] out Symbol symbol)
        {
            if (_globalSymbols.TryLookup(symbolReference, out symbol))
                return true;

            if (symbolReference.NamespaceName is null)
                return false; //If the reference has no namespace it must be a global symbol or can not be resolved

            if (!_data.TryGetValue(symbolReference.NamespaceName, out var cache))
                return false;

            return cache.TryLookup(symbolReference, out symbol);
        }
        
        public void AddSymbols(IEnumerable<Symbol> symbols)
        {
            foreach(var symbol in symbols)
                AddSymbol(symbol);
        }

        public void AddSymbol(Symbol symbol)
        {
            if (symbol.Namespace is null)
                AddDefaultSymbol(symbol);
            else
                AddConcreteSymbol(symbol);
        }

        private void AddDefaultSymbol(Symbol symbol)
        {
            Debug.Assert(
                condition: symbol.Namespace is null, 
                message: "A default symbol is not allowed to have a namespace"
            );
            
            _globalSymbols.Add(symbol);
        }
        
        private void AddConcreteSymbol(Symbol symbol)
        {
            Debug.Assert(
                condition: symbol.Namespace is not null, 
                message: "A concrete symbol is must have a namespace"
            );
            
            if (!_data.TryGetValue(symbol.Namespace.Name, out var cache))
            {
                cache = new SymbolCache();
                _data[symbol.Namespace.Name] = cache;
            }

            cache.Add(symbol);
        }
    }
}
