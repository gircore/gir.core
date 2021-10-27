using System.Collections.Generic;

namespace GirModel
{
    public interface Bitfield : ComplexType
    {
        Method? TypeFunction { get; }
        public IEnumerable<Member> Members { get; }
    }
}
