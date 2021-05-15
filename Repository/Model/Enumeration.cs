using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
{
    public class Enumeration : Type
    {
        public bool HasFlags { get; }
        public IEnumerable<Member> Members { get; }

        public Enumeration(Repository repository, CTypeName? cTypeName, TypeName typeName, SymbolName symbolName, bool hasFlags, IEnumerable<Member> members) : base(repository, cTypeName, typeName, symbolName)
        {
            HasFlags = hasFlags;
            Members = members;
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
            => Members.SelectMany(x => x.GetTypeReferences());

        public override bool GetIsResolved()
            => Members.AllResolved();
    }
}
