using System.Collections.Generic;

namespace Generator3.Model.Native
{
    public class Function
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;

        public GirModel.Method Model { get; }

        public string Name => Model.Name;
        public ReturnType ReturnType => _returnType ??= Model.ReturnType.CreateNativeModel();
        public string CIdentifier => Model.CIdentifier;
        public string NameSpaceName => Model.Namespace.Name;

        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.CreateNativeModels();

        public Function(GirModel.Method function)
        {
            Model = function;
        }
    }
}
