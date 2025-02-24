using System.Collections.Generic;

namespace GirModel;

public interface Enumeration : ComplexType
{
    Method? TypeFunction { get; }
    IEnumerable<Member> Members { get; }
    bool Introspectable { get; }
}
