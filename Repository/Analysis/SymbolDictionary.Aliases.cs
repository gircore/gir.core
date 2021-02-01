using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Repository.Analysis
{
    public partial class SymbolDictionary
    {
        // Maps Alias -> Symbol Name
        // Namespace-specific
        private class AliasDictionary
        {
            private readonly Dictionary<string, string> _aliasDict = new();
            private string Namespace { get; }

            public AliasDictionary(string nspace)
            {
                Namespace = nspace;
            }
        
            public void AddAlias(string from, string to)
                => _aliasDict.Add(from, to);

            public bool TryGetAlias(string name, [NotNullWhen(true)] out string alias)
                => _aliasDict.TryGetValue(name, out alias);
        }
    }
}
