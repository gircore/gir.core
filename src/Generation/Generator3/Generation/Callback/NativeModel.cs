using System.Collections.Generic;
using System.Linq;
using Generator3.Model;

namespace Generator3.Generation.Callback
{
    public class NativeModel
    {
        private readonly GirModel.Callback _callback;
        private ReturnType? _returnType;
        private IEnumerable<Parameter>? _parameters;

        public string Name => _callback.Name;
        public string NamespaceName => _callback.NamespaceName + ".Native";

        public ReturnType ReturnType => _returnType ??= ReturnType.CreateNative(_callback.ReturnType);
        public IEnumerable<Parameter> Parameters => _parameters ??= _callback.Parameters.Select(Parameter.CreateForNativeCallback);
        
        public NativeModel(GirModel.Callback callback)
        {
            _callback = callback;
        }
    }
}
