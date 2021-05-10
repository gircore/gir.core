using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
{
    public class Enumeration : Type
    {
        public bool HasFlags { get; }
        public IEnumerable<Member> Members { get; }

        public Enumeration(Namespace @namespace, CTypeName? cTypeName, TypeName typeName, SymbolName symbolName, bool hasFlags, IEnumerable<Member> members) : base(@namespace, cTypeName, typeName, symbolName)
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
