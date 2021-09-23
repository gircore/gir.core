using System.Collections.Generic;

namespace GirModel
{
    public interface Method
    {
        public string NamespaceName { get; } //TODO: Only valid for functions. Differentiate between method / function
        public string Name { get; }
        public Type ReturnType { get; }
        public string CIdentifier { get; }
        public IEnumerable<Parameter> Parameters { get; }
    }
}
