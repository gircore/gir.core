using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Repository.Model;

namespace Repository.Analysis
{
    public partial class SymbolDictionary
    {
        // Maps Symbol Name -> Introspection Data
        // Namespace-specific
        private class TypeDictionary
        {
            private readonly Dictionary<string, IType> _typeDict = new();
            private string? Namespace { get; }

            public TypeDictionary(string? nspace)
            {
                Namespace = nspace;
            }

            public void AddType(string name, IType info)
                => _typeDict.Add(name, info);

            public IType GetType(string name)
                => _typeDict[name];

            public bool TryGetType(string name, [NotNullWhen(true)] out IType? type)
                => _typeDict.TryGetValue(name, out type);
        }
    }
}
