using System.Collections.Generic;

namespace CWrapper
{
    public interface Struct
    {
        string Name { get; }
        IEnumerable<Method> Methods { get; }
    }
}