using System.Collections.Generic;

namespace GirModel
{
    public interface Union : ComplexType
    {
        Function? TypeFunction { get; }
        IEnumerable<Function> Functions { get; }
        IEnumerable<Method> Methods { get; }
        IEnumerable<Constructor> Constructors { get; }
        IEnumerable<Field> Fields { get; }
        bool Introspectable { get; }
    }
}
