using System.Collections.Generic;

namespace GirModel
{
    public interface Constructor
    {
        public string Name { get; }
        public ReturnType ReturnType { get; }
        public string CIdentifier { get; }
        public IEnumerable<Parameter> Parameters { get; }
    }
}
