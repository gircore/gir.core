using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class NativeFunction
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;

        public GirModel.Method Model { get; }

        public string Name => Model.Name;
        public ReturnType ReturnType => _returnType ??= ReturnType.CreateNative(Model.ReturnType);
        public string CIdentifier => Model.CIdentifier;
        public string NameSpaceName => Model.NamespaceName;

        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.Select(Parameter.CreateForNative);

        public NativeFunction(GirModel.Method function)
        {
            Model = function;
        }
    }
}
