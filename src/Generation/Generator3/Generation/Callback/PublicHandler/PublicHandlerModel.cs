using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Internal;

namespace Generator3.Generation.Callback
{
    public class PublicHandlerModel
    {
        private readonly Model.Internal.Callback _internalCallback;
        private readonly GirModel.Callback _callback;
        private Model.Public.StandardReturnType? _returnType;
        private IEnumerable<Model.Public.StandardParameter>? _publicParameters;
        private IEnumerable<Model.Internal.Parameter>? _nativeParameters;

        public string Name => _callback.Name + "Handler";
        public string DelegateType => _callback.Name;
        public string InternalDelegateType => _callback.Namespace.GetInternalName() + "." + _callback.Name;
        
        public string NamespaceName => _callback.Namespace.Name;

        public Model.Public.StandardReturnType ReturnType => _returnType ??= new Model.Public.StandardReturnType(_callback.ReturnType);
        public Model.Internal.ReturnType InternalReturnType => _internalCallback.ReturnType;
        public IEnumerable<Model.Public.StandardParameter> PublicParameters => _publicParameters ??= _callback.Parameters.Select(x => new Model.Public.StandardParameter(x));
        public IEnumerable<Parameter> NativeParameters => _nativeParameters ??= _callback.Parameters.CreateInternalModelsForCallback();

        
        public PublicHandlerModel(GirModel.Callback callback)
        {
            _internalCallback = new Model.Internal.Callback(callback);
            _callback = callback;
        }
    }
}
