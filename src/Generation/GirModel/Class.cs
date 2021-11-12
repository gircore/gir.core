using System.Collections.Generic;

namespace GirModel
{
    public interface Class : ComplexType
    {
        Method TypeFunction { get; }
        bool Fundamental { get; }
        IEnumerable<Field> Fields { get; }
    }
}
