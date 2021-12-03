using System.Collections.Generic;

namespace GirModel
{
    public interface Method
    {
        public string Name { get; }//TODO Remove as name resolution should be part of the generator
        public ReturnType ReturnType { get; }
        public string CIdentifier { get; }
        public InstanceParameter InstanceParameter { get; }
        public IEnumerable<Parameter> Parameters { get; }
    }
}
