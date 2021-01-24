using System.Collections.Generic;
using Repository.Analysis;

#nullable enable

namespace Repository.Model
{
    public record Class : ISymbol
    {
        public Namespace? Namespace { get; init; }
        public string ManagedName { get; set; }
        public string NativeName { get; init; }
        
        public string CType { get; init; }
        public TypeReference? Parent { get; init; }
        public List<TypeReference> Implements { get; init; }
    }
}
