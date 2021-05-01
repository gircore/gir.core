using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Enumeration : Symbol
    {
        public bool HasFlags { get; }
        public IEnumerable<Member> Members { get; }

        public Enumeration(Namespace @namespace, CTypeName? cTypeName, TypeName typeName, SymbolName symbolName, bool hasFlags, IEnumerable<Member> members) : base(@namespace, cTypeName, typeName, symbolName)
        {
            HasFlags = hasFlags;
            Members = members;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
            => Members.SelectMany(x => x.GetSymbolReferences());

        public override bool GetIsResolved()
            => Members.AllResolved();
    }
}
