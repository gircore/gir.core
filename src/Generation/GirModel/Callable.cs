using System.Collections.Generic;

namespace GirModel;

public interface Callable
{
    string Name { get; }
    bool Throws { get; }
    ReturnType ReturnType { get; }
    IEnumerable<Parameter> Parameters { get; }
    InstanceParameter? InstanceParameter { get; }
    Callable? Shadows { get; }
    Callable? ShadowedBy { get; }
}
