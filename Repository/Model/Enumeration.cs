using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Enumeration : Type
    {
        public bool HasFlags { get; }
        public IEnumerable<Member> Members { get; }
        
        public Enumeration(Namespace @namespace, string nativeName, string managedName, bool hasFlags, IEnumerable<Member> members) : base(@namespace, nativeName, managedName)
        {
            HasFlags = hasFlags;
            Members = members;
        }

        public override IEnumerable<ISymbolReference> GetSymbolReferences()
            => Members.SelectMany(x => x.GetSymbolReferences());
    }
}
