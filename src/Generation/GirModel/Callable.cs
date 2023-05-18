using System.Collections.Generic;

namespace GirModel;

public interface Callable
{
    string Name { get; }
    bool Throws { get; }
    IEnumerable<Parameter> Parameters { get; }
    InstanceParameter? InstanceParameter { get; }
    Callable? Shadows { get; }
    Callable? ShadowedBy { get; }
}
