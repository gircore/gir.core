using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class EnumerationUnit
    {
        private readonly GirModel.Enumeration _enumeration;

        public string Name => _enumeration.Name;
        public string NamespaceName => _enumeration.NamespaceName;
        public IEnumerable<Member> Members { get; }

        public EnumerationUnit(GirModel.Enumeration enumeration)
        {
            _enumeration = enumeration;

            Members = enumeration.Members
                .Select(member => new Member(member))
                .ToList();
        }
    }
}
