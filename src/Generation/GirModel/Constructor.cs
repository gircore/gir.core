using System.Collections.Generic;

namespace GirModel;

public interface Constructor
{
    string Name { get; }
    ReturnType ReturnType { get; }
    string CIdentifier { get; }
    IEnumerable<Parameter> Parameters { get; }
    string? Version { get; }
}
