using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Internal;

namespace Generator3.Generation.Union
{
    public class InternalMethodsModel
    {
        private readonly GirModel.Union _union;

        public string Name => _union.Name;
        public string NamespaceName => _union.Namespace.GetInternalName();
        public IEnumerable<Function> Functions { get; }
        public IEnumerable<Method> Methods { get; }
        public IEnumerable<Constructor> Constructors { get; }
        public Function? TypeFunction { get; }

        public InternalMethodsModel(GirModel.Union union)
        {
            _union = union;

            Functions = union.Functions
                .Select(function => new Function(function))
                .ToList();

            Methods = union.Methods
                .Select(method => new Method(method, union.Namespace.Name))
                .ToList();

            Constructors = union.Constructors
                .Select(method => new Constructor(method, union.Namespace.Name))
                .ToList();

            TypeFunction = union.TypeFunction is not null
                ? new Function(union.TypeFunction)
                : null;
        }
    }
}
