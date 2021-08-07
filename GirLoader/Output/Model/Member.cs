using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output.Model
{
    public class Member : Symbol
    {
        public string Value { get; }

        public Member(SymbolName originalName, SymbolName symbolName, string value) : base(originalName, symbolName)
        {
            Value = value;
        }

        internal override IEnumerable<TypeReference> GetTypeReferences()
            => Enumerable.Empty<TypeReference>();

        internal override bool GetIsResolved()
            => true;
    }
}
