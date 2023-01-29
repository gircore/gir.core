using System.Collections.Generic;

namespace GirModel;

public interface Signal
{
    string Name { get; }
    IEnumerable<Parameter> Parameters { get; }
    ReturnType ReturnType { get; }
    bool Introspectable { get; }
}
