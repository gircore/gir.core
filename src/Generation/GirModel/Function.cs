using System.Collections.Generic;

namespace GirModel;

public interface Function : Callable
{
    public Namespace Namespace { get; }

    /// <summary>
    /// The containing type of this function. If null it is a global function.
    /// </summary>
    ComplexType? Parent { get; }
    public ReturnType ReturnType { get; }
    public bool Introspectable { get; }
    string? Version { get; }
}
