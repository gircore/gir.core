using System.Collections.Generic;

namespace GirModel
{
    public interface Enumeration : ComplexType
    {
        Method? TypeFunction { get; }
        public IEnumerable<Member> Members { get; }
    }
}
