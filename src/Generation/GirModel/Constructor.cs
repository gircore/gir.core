using System.Collections.Generic;

namespace GirModel;

public interface Constructor : Callable
{
    ReturnType ReturnType { get; }
    string? Version { get; }
}
