using System.Collections.Generic;

namespace GirModel
{
    public interface Enumeration : ComplexType
    {
        public IEnumerable<Member> Members { get; }
    }
}
