using System.Collections.Generic;

namespace GirModel;

public interface Function
{
    public Namespace Namespace { get; }

    /// <summary>
    /// The containing type of this function. If null it is a global function.
    /// </summary>
    ComplexType? Parent { get; }
    public string Name { get; }
    public ReturnType ReturnType { get; }
    public string CIdentifier { get; }
    public IEnumerable<Parameter> Parameters { get; }
    public bool Introspectable { get; }
    string? Version { get; }
}
