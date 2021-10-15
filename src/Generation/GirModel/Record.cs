using System.Collections.Generic;

namespace GirModel
{
    public interface Record : ComplexType
    {
        IEnumerable<Method> Functions { get; }
    }
}
