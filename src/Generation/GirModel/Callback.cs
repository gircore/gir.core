using System.Collections.Generic;

namespace GirModel
{
    public interface Callback : ComplexType
    {
        ReturnType ReturnType { get; }
        IEnumerable<Parameter> Parameters { get; }
        bool Introspectable { get; }
    }
}
