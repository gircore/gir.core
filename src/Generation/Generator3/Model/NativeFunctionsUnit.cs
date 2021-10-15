using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class NativeFunctionsUnit
    {
        public IEnumerable<GirModel.Method> Functions { get; }

        public string NamespaceName => Functions.First().NamespaceName + ".Native";
        public IEnumerable<NativeFunction> NativeFunctions { get; }

        public NativeFunctionsUnit(IEnumerable<GirModel.Method> functions)
        {
            Functions = functions;

            NativeFunctions = functions
                .Select(function => new NativeFunction(function))
                .ToList();
        }
    }
}
