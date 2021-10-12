using System.Collections.Generic;
using System.Linq;

namespace Generator3.Generation.Model
{
    public class NativeFunction
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnValue? _returnValue;

        public GirModel.Method Model { get; }

        public string Name => Model.Name;

        public string ReturnTypeName
        {
            get
            {
                _returnValue ??= ReturnValue.CreateNative(Model.ReturnValue);
                return _returnValue.Code;
            }
        }
        public string CIdentifier => Model.CIdentifier;
        public string NameSpaceName => Model.NamespaceName;

        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.Select(Parameter.CreateNative);

        public NativeFunction(GirModel.Method function)
        {
            Model = function;
        }
    }
}
