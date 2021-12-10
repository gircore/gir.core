using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Enumeration : ComplexType
    {
        public IEnumerable<Member> Members { get; }

        public Enumeration(Repository repository, string? cType, TypeName originalName, TypeName name, IEnumerable<Member> members) : base(repository, cType, originalName)
        {
            Members = members;
        }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.SymbolNameReference is not null)
            {
                var namespaceOk = typeReference.SymbolNameReference.NamespaceName == Repository.Namespace.Name
                                  || typeReference.SymbolNameReference.NamespaceName == null;

                return namespaceOk && typeReference.SymbolNameReference.SymbolName == OriginalName;
            }

            if (typeReference.CTypeReference is not null)
                return typeReference.CTypeReference.CType == CType;

            return false;
        }
    }
}
