using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class SymbolDictionary
    {
        private readonly Dictionary<NamespaceName, AliasCache> _aliasData = new();
        private readonly Dictionary<NamespaceName, SymbolCache> _data = new();
        private readonly SymbolCache _globalSymbols = new();

        public bool TryLookup(SymbolReference symbolReference, [MaybeNullWhen(false)] out Symbol symbol)
        {
            if (_globalSymbols.TryLookup(symbolReference, out symbol))
                return true;

            if (ResolveAlias(symbolReference, out symbol))
                return true;
            
            if (symbolReference.NamespaceName is null)
                return false; //If the reference has no namespace it must be a global symbol or can not be resolved

            if (!_data.TryGetValue(symbolReference.NamespaceName, out var cache))
                return false;

            return cache.TryLookup(symbolReference, out symbol);
        }

        private bool ResolveAlias(SymbolReference symbolReference, [MaybeNullWhen(false)] out Symbol symbol)
        {
            symbol = null;
            
            if (symbolReference.NamespaceName is null)
                return false;

            if (!_aliasData.TryGetValue(symbolReference.NamespaceName, out var aliasCache))
                return false;

            if (!aliasCache.TryLookup(symbolReference, out var alias))
                return false;

            symbol = alias.SymbolReference.GetSymbol();
            return true;
        }
        
        public void AddSymbols(IEnumerable<Symbol> symbols)
        {
            foreach(var symbol in symbols)
                AddSymbol(symbol);
        }

        public void AddSymbol(Symbol symbol)
        {
            if (symbol.Namespace is null)
                AddGlobalSymbol(symbol);
            else
                AddConcreteSymbol(symbol);
        }

        private void AddGlobalSymbol(Symbol symbol)
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

        public void AddAliases(IEnumerable<Alias> aliases)
        {
            foreach(var alias in aliases)
                AddAlias(alias);
        }

        private void AddAlias(Alias alias)
        {
            if (!_aliasData.TryGetValue(alias.Namespace.Name, out var cache))
            {
                cache = new AliasCache();
                _aliasData[alias.Namespace.Name] = cache;
            }

            cache.Add(alias);
        }

        public void ResolveAliases()
        {
            foreach(var aliasData in _aliasData.Values)
                foreach (var alias in aliasData.Aliases)
                    if(TryLookup(alias.SymbolReference, out var symbol))
                        alias.SymbolReference.ResolveAs(symbol);
        }
    }
}
