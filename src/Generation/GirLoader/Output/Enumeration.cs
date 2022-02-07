using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Enumeration : ComplexType
    {
        public IEnumerable<Member> Members { get; }
        public bool Introspectable { get; }

        public Enumeration(Repository repository, string? cType, string name, IEnumerable<Member> members, bool introspectable) : base(repository, cType, name)
        {
            Members = members;
            Introspectable = introspectable;
        }

        internal override bool Matches(TypeReference typeReference)
        {
            var ctypeMatches = typeReference.CTypeReference?.CType == CType;
            var symbolNameMatches = typeReference.SymbolNameReference?.SymbolName == Name;
            var namespaceMatches = typeReference.SymbolNameReference?.NamespaceName == Repository.Namespace.Name
                                   || typeReference.SymbolNameReference?.NamespaceName == null;

            return ctypeMatches || (namespaceMatches && symbolNameMatches);
        }
    }
}
