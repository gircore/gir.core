using System.Collections.Generic;

namespace Generator3.Model
{
    public class NativeMethod
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;

        public GirModel.Method Model { get; }

        public string Name => Model.Name;
        public ReturnType ReturnType => _returnType ??= Model.ReturnType.CreateNativeModel();
        public string CIdentifier => Model.CIdentifier;
        public string NameSpaceName => Model.NamespaceName;

        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.CreateNativeModelsForMethod();

        public NativeMethod(GirModel.Method function)
        {
            Model = function;
        }
    }
}
