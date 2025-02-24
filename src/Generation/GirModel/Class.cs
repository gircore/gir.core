using System.Collections.Generic;

namespace GirModel;

public interface Class : ComplexType
{
    Class? Parent { get; }
    IEnumerable<Interface> Implements { get; }
    Function TypeFunction { get; }
    bool Fundamental { get; }
    bool Abstract { get; }
    bool Final { get; }
    IEnumerable<Field> Fields { get; }
    IEnumerable<Function> Functions { get; }
    IEnumerable<Method> Methods { get; }
    IEnumerable<Constructor> Constructors { get; }
    IEnumerable<Property> Properties { get; }
    IEnumerable<Signal> Signals { get; }
    bool Introspectable { get; }
}
