using System.Collections.Generic;

namespace GirModel
{
    public interface Record : ComplexType
    {
        Method? TypeFunction { get; }
        IEnumerable<Method> Functions { get; }
        IEnumerable<Field> Fields { get; }
    }
}
