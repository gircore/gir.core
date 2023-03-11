using System.Collections.Generic;

namespace GirModel;

public interface Interface : ComplexType
{
    Function TypeFunction { get; }
    IEnumerable<Constructor>? Constructors { get; }
    IEnumerable<Function> Functions { get; }
    IEnumerable<Method> Methods { get; }
    IEnumerable<Property> Properties { get; }
    bool Introspectable { get; }
}
