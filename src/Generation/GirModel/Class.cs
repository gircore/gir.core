using System.Collections.Generic;

namespace GirModel
{
    public interface Class : ComplexType
    {
        Class? Parent { get; }
        Function TypeFunction { get; }
        bool IsFundamental { get; }
        IEnumerable<Field> Fields { get; }
        IEnumerable<Function> Functions { get; }
        IEnumerable<Method> Methods { get; }
        IEnumerable<Constructor> Constructors { get; }
        IEnumerable<Property> Properties { get; }
        IEnumerable<Signal> Signals { get; }
        bool Introspectable { get; }
    }
}
