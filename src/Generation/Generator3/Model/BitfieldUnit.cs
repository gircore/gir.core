using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class BitfieldUnit
    {
        private readonly GirModel.Bitfield _bitfield;

        public string Name => _bitfield.Name;
        public string NamespaceName => _bitfield.NamespaceName;
        public IEnumerable<Member> Members { get; }

        public BitfieldUnit(GirModel.Bitfield bitfield)
        {
            _bitfield = bitfield;

            Members = bitfield.Members
                .Select(member => new Member(member))
                .ToList();
        }
    }
}
