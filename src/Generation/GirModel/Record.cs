using System.Collections.Generic;

namespace GirModel
{
    public interface Record : ComplexType
    {
        Method? TypeFunction { get; }
        IEnumerable<Method> Functions { get; }
        IEnumerable<Method> Methods { get; }
        IEnumerable<Method> Constructors { get; }
        IEnumerable<Field> Fields { get; }
    }
}
