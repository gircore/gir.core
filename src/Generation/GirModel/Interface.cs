using System.Collections.Generic;

namespace GirModel
{
    public interface Interface : ComplexType
    {
        Method TypeFunction { get; }
        IEnumerable<Method> Functions { get; }
        IEnumerable<Method> Methods { get; }
    }
}
