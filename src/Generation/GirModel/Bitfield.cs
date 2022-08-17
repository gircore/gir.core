using System.Collections.Generic;

namespace GirModel
{
    public interface Bitfield : ComplexType
    {
        Method? TypeFunction { get; }
        IEnumerable<Member> Members { get; }
        bool Introspectable { get; }
    }
}
