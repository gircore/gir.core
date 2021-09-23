using System.Collections.Generic;
using GirModel;

namespace Generator3.Generation.NativeFunctions
{
    public class Data
    {
        private readonly HashSet<NativeFunction> _functions = new();
        
        public string NamespaceName { get; }
        public IEnumerable<NativeFunction> NativeFunctions => _functions;

        public Data(string namespaceName)
        {
            NamespaceName = namespaceName;
        }

        public void Add(NativeFunction nativeFunction)
            => _functions.Add(nativeFunction);
        
    }
}
