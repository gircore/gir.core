using System.Collections.Generic;
using GirModel;

namespace Generator3.Generation.NativeFunctions
{
    public class Data
    {
        private readonly HashSet<Code.NativeFunction> _functions = new();
        
        public string NamespaceName { get; }
        public IEnumerable<Code.NativeFunction> NativeFunctions => _functions;

        public Data(string namespaceName)
        {
            NamespaceName = namespaceName;
        }

        public void Add(Code.NativeFunction nativeFunction)
            => _functions.Add(nativeFunction);
        
    }
}
