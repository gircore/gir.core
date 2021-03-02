using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Member : Symbol
    {
        public string Value { get; }

        public Member(string name, string managedName, string value) : base(name, managedName)
        {
            Value = value;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
            => Enumerable.Empty<SymbolReference>();
    }
}
