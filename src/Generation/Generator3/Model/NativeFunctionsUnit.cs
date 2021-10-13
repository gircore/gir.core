using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class NativeFunctionsUnit
    {
        public IEnumerable<GirModel.Method> Functions { get; }
        private readonly HashSet<NativeFunction> _functions = new();

        public string NamespaceName => Functions.First().NamespaceName + ".Native";
        public IEnumerable<NativeFunction> NativeFunctions => _functions;

        public NativeFunctionsUnit(IEnumerable<GirModel.Method> functions)
        {
            Functions = functions;
            
            foreach (var function in functions)
                Add(new NativeFunction(function));
        }

        private void Add(NativeFunction nativeFunction)
            => _functions.Add(nativeFunction);

    }
}
