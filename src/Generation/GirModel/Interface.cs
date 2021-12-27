using System.Collections.Generic;

namespace GirModel
{
    public interface Interface : ComplexType
    {
        Function TypeFunction { get; }
        IEnumerable<Function> Functions { get; }
        IEnumerable<Method> Methods { get; }
        bool Introspectable { get; }
    }
}
