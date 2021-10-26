using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class NativeCallbackUnit
    {
        private readonly GirModel.Callback _callback;
        private ReturnType? _returnType;
        private IEnumerable<Parameter>? _parameters;

        public string Name => _callback.Name;
        public string NamespaceName => _callback.NamespaceName + ".Native";

        public ReturnType ReturnType => _returnType ??= ReturnType.CreateNative(_callback.ReturnType);
        public IEnumerable<Parameter> Parameters => _parameters ??= _callback.Parameters.Select(Parameter.CreateForNativeCallback);
        
        public NativeCallbackUnit(GirModel.Callback callback)
        {
            _callback = callback;
        }
    }
}
