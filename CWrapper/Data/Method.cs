using System.Collections.Generic;

namespace CWrapper
{
    public interface Method
    {
        string Name { get; }
        string ReturnType { get; }
        string Import { get; }
        string EntryPoint { get; }
        string? Summary{ get; }
        bool Obsolete { get; }
        string? ObsoleteSummary { get; }

        IEnumerable<Parameter> Parameters { get; }
    }
}