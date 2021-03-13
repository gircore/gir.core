using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Repository.Model;

namespace Repository.Analysis
{
    internal partial class SymbolDictionary
    {
        private class TypeDictionary
        {
            private readonly List<Symbol> _aliasDict = new();

            public void AddAlias(Symbol alias)
                => _aliasDict.Add(alias);

            public bool TryGetAlias(string? name, string? ctype, [NotNullWhen(true)] out string? alias)
            {
                alias = _aliasDict.FirstOrDefault(x => Check(x, name, ctype))?.Name;

                return alias is not null;
            }

            private bool Check(Symbol symbol, string? name, string? ctype)
            {
                if (name is not null && symbol.Name == name)
                    return true;

                if (ctype is not null && symbol.CName == ctype)
                    return true;

                return false;
            }
        }
    }
}
