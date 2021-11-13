using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Native;

namespace Generator3.Generation.Functions
{
    public class NativeModel
    {
        public IEnumerable<GirModel.Method> Functions { get; }

        public string NamespaceName => Functions.First().Namespace.GetNativeName();
        public IEnumerable<Function> NativeFunctions { get; }

        public NativeModel(IEnumerable<GirModel.Method> functions)
        {
            Functions = functions;

            NativeFunctions = functions
                .Select(function => new Function(function))
                .ToList();
        }
    }
}
