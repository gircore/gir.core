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
            if (typeReference.SymbolNameReference is not null)
            {
                var namespaceOk = typeReference.SymbolNameReference.NamespaceName == Repository.Namespace.Name
                                  || typeReference.SymbolNameReference.NamespaceName == null;

                return namespaceOk && typeReference.SymbolNameReference.SymbolName == Name;
            }

            if (typeReference.CTypeReference is not null)
                return typeReference.CTypeReference.CType == CType;

            return false;
        }
    }
}
