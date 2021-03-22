using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class SymbolDictionary
    {
        private readonly Dictionary<NamespaceName, SymbolCache> _data = new();
        private readonly SymbolCache _globalSymbols = new(null);

        public bool TryLookup(SymbolReference symbolReference, [MaybeNullWhen(false)] out Symbol symbol)
        {
            if (_globalSymbols.TryLookup(symbolReference, out symbol))
                return true;

            if (TryResolveAlias(symbolReference, out symbol))
                return true;
            
            if (symbolReference.NamespaceName is null)
                return false; //If the reference has no namespace it must be a global symbol or can not be resolved

            if (!_data.TryGetValue(symbolReference.NamespaceName, out var cache))
                return false;

            return cache.TryLookup(symbolReference, out symbol);
        }

        private bool TryResolveAlias(SymbolReference symbolReference, [MaybeNullWhen(false)] out Symbol symbol)
        {
            symbol = null;
            
            if (symbolReference.NamespaceName is null)
                return false;

            if (!_data.TryGetValue(symbolReference.NamespaceName, out var symbolCache))
                return false;

            var ns = symbolCache.Namespace;

            if (ns is null)
                throw new Exception("Namespace is missing");

            symbol = RecursiveResolveAlias(ns, symbolReference);
            
            return symbol is {};
        }

        private Symbol? RecursiveResolveAlias(Namespace ns, SymbolReference symbolReference)
        {
            var directResult = ns.Aliases.FirstOrDefault(x => Resolves(x, symbolReference));

            if (directResult is { })
                return directResult.SymbolReference.GetSymbol();

            foreach (var parent in ns.Dependencies)
            {
                var parentResult = RecursiveResolveAlias(parent, symbolReference);
                if (parentResult is { })
                    return parentResult;
            }

            return null;
        }

        private static bool Resolves(Alias alias, SymbolReference symbolReference)
        {
            return (symbolReference.TypeName == alias.SymbolName) || (symbolReference.CTypeName == alias.Name);
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
                cache = new SymbolCache(symbol.Namespace);
                _data[symbol.Namespace.Name] = cache;
            }

            cache.Add(symbol);
        }

        public void ResolveAliases(IEnumerable<Alias> aliases)
        {
            foreach (var alias in aliases)
            {
                if (TryLookup(alias.SymbolReference, out var symbol))
                    alias.SymbolReference.ResolveAs(symbol);
            }
        }
    }
}
