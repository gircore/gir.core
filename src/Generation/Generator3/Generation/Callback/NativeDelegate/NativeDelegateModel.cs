using System.Collections.Generic;
using Generator3.Model.Native;

namespace Generator3.Generation.Callback
{
    public class NativeDelegateModel
    {
        private readonly GirModel.Callback _callback;
        private ReturnType? _returnType;
        private IEnumerable<Parameter>? _parameters;

        public string Name => _callback.Name;
        public string NamespaceName => _callback.Namespace.GetNativeName();

        public ReturnType ReturnType => _returnType ??= _callback.ReturnType.CreateNativeModel();
        public IEnumerable<Parameter> Parameters => _parameters ??= _callback.Parameters.CreateNativeModelsForCallback();
        
        public NativeDelegateModel(GirModel.Callback callback)
        {
            _callback = callback;
        }
    }
}
