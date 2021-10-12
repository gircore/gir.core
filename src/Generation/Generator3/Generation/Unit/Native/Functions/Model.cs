using System.Collections.Generic;
using GirModel;

namespace Generator3.Generation.Unit.Native.Functions
{
    public class Model
    {
        private readonly HashSet<Generation.Model.NativeFunction> _functions = new();

        public string NamespaceName { get; }
        public IEnumerable<Generation.Model.NativeFunction> NativeFunctions => _functions;

        public Model(string namespaceName)
        {
            NamespaceName = namespaceName;
        }

        public void Add(Generation.Model.NativeFunction nativeFunction)
            => _functions.Add(nativeFunction);

    }
}
