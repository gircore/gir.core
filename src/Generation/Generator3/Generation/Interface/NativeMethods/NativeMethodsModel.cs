using System.Collections.Generic;
using System.Linq;
using Generator3.Model;

namespace Generator3.Generation.Interface
{
    public class NativeMethodsModel
    {
        private readonly GirModel.Interface _interface;

        public string Name => _interface.Name;
        public string NamespaceName => _interface.Namespace.Name + ".Native";
        public IEnumerable<NativeFunction> Functions { get; }
        public IEnumerable<NativeMethod> Methods { get; }
        public NativeFunction? TypeFunction { get; }

        public NativeMethodsModel(GirModel.Interface @interface)
        {
            _interface = @interface;

            Functions = @interface.Functions
                .Select(function => new NativeFunction(function))
                .ToList();

            Methods = @interface.Methods
                .Select(method => new NativeMethod(method))
                .ToList();

            TypeFunction = @interface.TypeFunction is not null
                ? new NativeFunction(@interface.TypeFunction)
                : null;
        }
    }
}
