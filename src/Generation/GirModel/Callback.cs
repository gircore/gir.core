using System.Collections.Generic;

namespace GirModel
{
    public interface Callback : ComplexType
    {
        public ReturnType ReturnType { get; }
        public IEnumerable<Parameter> Parameters { get; }
    }
}
