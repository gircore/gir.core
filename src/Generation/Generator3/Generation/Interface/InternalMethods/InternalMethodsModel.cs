using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Internal;

namespace Generator3.Generation.Interface
{
    public class InternalMethodsModel
    {
        private readonly GirModel.Interface _interface;

        public string Name => _interface.Name;
        public string NamespaceName => _interface.Namespace.GetInternalName();
        public GirModel.PlatformDependent? PlatformDependent => _interface as GirModel.PlatformDependent;

        public IEnumerable<Function> Functions { get; }
        public IEnumerable<Method> Methods { get; }
        public Function? TypeFunction { get; }

        public InternalMethodsModel(GirModel.Interface @interface)
        {
            _interface = @interface;

            Functions = @interface.Functions
                .Select(function => new Function(function))
                .ToList();

            Methods = @interface.Methods
                .Select(method => new Method(method, @interface.Namespace.Name, @interface.Name))
                .ToList();

            TypeFunction = new Function(@interface.TypeFunction);
        }
    }
}
