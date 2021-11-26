using System.Collections.Generic;

namespace GirModel
{
    public interface Method
    {
        public string Name { get; }
        public ReturnType ReturnType { get; }
        public string CIdentifier { get; }
        public InstanceParameter InstanceParameter { get; }
        public IEnumerable<Parameter> Parameters { get; }
    }
}
