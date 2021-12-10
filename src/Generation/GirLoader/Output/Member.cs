using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public partial class Member : Symbol
    {
        public string Value { get; }

        public Member(string originalName, string value) : base(originalName)
        {
            Value = value;
        }

        internal override IEnumerable<TypeReference> GetTypeReferences()
            => Enumerable.Empty<TypeReference>();

        internal override bool GetIsResolved()
            => true;
    }
}
