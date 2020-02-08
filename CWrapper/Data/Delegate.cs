using System.Collections.Generic;

namespace CWrapper
{
    public interface Delegate
    {
        string Name { get; }
        string ReturnType { get; }

        IEnumerable<Parameter> Parameters { get; }
    }
}