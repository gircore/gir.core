using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Member : Symbol
    {
        public string Value { get; }

        public Member(string nativeName, string managedName, string value) : base(nativeName, managedName)
        {
            Value = value;
        }

        public override IEnumerable<ISymbolReference> GetSymbolReferences()
            => Enumerable.Empty<ISymbolReference>();
    }
}
