using System.Collections.Generic;

namespace GirModel;

public interface Interface : ComplexType
{
    Function TypeFunction { get; }
    IEnumerable<Constructor>? Constructors { get; }
    IEnumerable<Function> Functions { get; }
    IEnumerable<Method> Methods { get; }
    IEnumerable<Property> Properties { get; }
    IEnumerable<Signal> Signals { get; }
    bool Introspectable { get; }
    IEnumerable<ComplexType> Prerequisites { get; }
}
