using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public partial class Bitfield : ComplexType
    {
        public IEnumerable<Member> Members { get; }

        public Bitfield(Repository repository, CType? cType, TypeName originalName, TypeName name, IEnumerable<Member> members) : base(repository, cType, name, originalName)
        {
            Members = members;
        }

        internal override IEnumerable<TypeReference> GetTypeReferences()
            => Members.SelectMany(x => x.GetTypeReferences());

        internal override bool GetIsResolved()
            => Members.All(x => x.GetIsResolved());

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
