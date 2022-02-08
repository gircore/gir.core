using System.Collections.Generic;

namespace GirLoader.Output;

public interface AccessorProvider
{
    IEnumerable<Method> Methods { get; }
    IEnumerable<Property> Properties { get; }
}
