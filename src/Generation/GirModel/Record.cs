using System.Collections.Generic;

namespace GirModel
{
    public interface Record : ComplexType
    {
        Function? TypeFunction { get; }
        IEnumerable<Function> Functions { get; }
        IEnumerable<Method> Methods { get; }
        IEnumerable<Constructor> Constructors { get; }
        IEnumerable<Field> Fields { get; }
    }
}
