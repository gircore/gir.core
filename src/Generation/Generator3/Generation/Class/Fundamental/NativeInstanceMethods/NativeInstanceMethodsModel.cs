using System.Collections.Generic;
using System.Linq;
using Generator3.Model;

namespace Generator3.Generation.Class.Fundamental
{
    public class NativeInstanceMethodsModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.Name + ".Native";
        
        public IEnumerable<NativeFunction> Functions { get; }
        public IEnumerable<NativeMethod> Methods { get; }
        public IEnumerable<NativeConstructor> Constructors { get; }
        public NativeFunction? TypeFunction { get; }
        
        public NativeInstanceMethodsModel(GirModel.Class @class)
        {
            _class = @class;

            Functions = @class.Functions
                .Select(function => new NativeFunction(function))
                .ToList();

            Methods = @class.Methods
                .Select(method => new NativeMethod(method))
                .ToList();

            Constructors = @class.Constructors
                .Select(method => new NativeConstructor(method))
                .ToList();

            TypeFunction = new NativeFunction(@class.TypeFunction);
        }
    }
}
