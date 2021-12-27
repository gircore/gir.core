using System.Collections.Generic;

namespace GirModel
{
    public interface Signal
    {
        string Name { get; }
        public IEnumerable<Parameter> Parameters { get; }
        bool Introspectable { get; }
    }
}
