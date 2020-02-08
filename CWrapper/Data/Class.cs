using System.Collections.Generic;

namespace CWrapper
{
    public interface Class
    {
        string Name { get; }
        IEnumerable<Method> Methods { get; }
    }
}