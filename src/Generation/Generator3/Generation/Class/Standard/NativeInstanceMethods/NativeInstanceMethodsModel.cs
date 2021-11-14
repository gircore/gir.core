using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Native;

namespace Generator3.Generation.Class.Standard
{
    public class NativeInstanceMethodsModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.GetNativeName();
        
        public IEnumerable<Function> Functions { get; }
        public IEnumerable<Method> Methods { get; }
        public IEnumerable<Constructor> Constructors { get; }
        public Function? TypeFunction { get; }
        
        public NativeInstanceMethodsModel(GirModel.Class @class)
        {
            _class = @class;

            Functions = @class.Functions
                .Select(function => new Function(function))
                .ToList();

            Methods = @class.Methods
                .Select(method => new Method(method))
                .ToList();

            Constructors = @class.Constructors
                .Select(method => new Constructor(method))
                .ToList();

            TypeFunction = new Function(@class.TypeFunction);
        }
    }
}
