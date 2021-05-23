using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;

namespace GirLoader.Output.Model
{
    public class Enumeration : Type
    {
        public bool HasFlags { get; }
        public IEnumerable<Member> Members { get; }

        public Enumeration(Repository repository, CTypeName? cTypeName, SymbolName originalName, SymbolName symbolName, bool hasFlags, IEnumerable<Member> members) : base(repository, cTypeName, originalName, symbolName)
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
