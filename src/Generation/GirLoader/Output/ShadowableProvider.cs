using System.Collections.Generic;

namespace GirLoader.Output;

public interface ShadowableProvider
{
    IEnumerable<Constructor>? Constructors { get; }
    IEnumerable<Method> Methods { get; }
    IEnumerable<Function> Functions { get; }
}
