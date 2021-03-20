using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class SymbolDictionary
    {
        private class AliasCache
        {
            private readonly HashSet<Alias> _aliases = new();

            public IEnumerable<Alias> Aliases => _aliases;
            
            public void Add(Alias alias)
            {
                _aliases.Add(alias);
            }
            
            public bool TryLookup(SymbolReference symbolReference, [MaybeNullWhen(false)] out Alias alias)
            {
                alias = _aliases.FirstOrDefault(x => CheckAlias(x, symbolReference));

                return alias is not null;
            }

            private bool CheckAlias(Alias alias, SymbolReference symbolReference)
            {
                return (symbolReference.TypeName == alias.SymbolName) || (symbolReference.CTypeName == alias.Name);
            }
        }
    }
}
