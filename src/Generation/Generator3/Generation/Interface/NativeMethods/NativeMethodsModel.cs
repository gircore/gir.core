using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Native;

namespace Generator3.Generation.Interface
{
    public class NativeMethodsModel
    {
        private readonly GirModel.Interface _interface;

        public string Name => _interface.Name;
        public string NamespaceName => _interface.Namespace.GetNativeName();
        public IEnumerable<Function> Functions { get; }
        public IEnumerable<Method> Methods { get; }
        public Function? TypeFunction { get; }

        public NativeMethodsModel(GirModel.Interface @interface)
        {
            _interface = @interface;

            Functions = @interface.Functions
                .Select(function => new Function(function))
                .ToList();

            Methods = @interface.Methods
                .Select(method => new Method(method))
                .ToList();

            TypeFunction = new Function(@interface.TypeFunction);
        }
    }
}
