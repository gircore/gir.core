using System.Collections.Generic;

namespace GirModel
{
    public interface Class : ComplexType
    {
        Class? Parent { get; }
        Method TypeFunction { get; }
        bool IsFundamental { get; }
        IEnumerable<Field> Fields { get; }
        IEnumerable<Method> Functions { get; }
        IEnumerable<Method> Methods { get; }
        IEnumerable<Method> Constructors { get; }
    }
}
