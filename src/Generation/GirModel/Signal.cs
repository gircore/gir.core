using System.Collections.Generic;

namespace GirModel
{
    public interface Signal
    {
        string ManagedName { get; } //TODO Remove the managed name should be calculated from the generator
        string NativeName { get; }
        public IEnumerable<Parameter> Parameters { get; }
    }
}
