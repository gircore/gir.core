using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Internal;
using Generator3.Model.Public;

namespace Generator3.Generation.Callback
{
    public class PublicHandlerModel
    {
        private readonly Model.Internal.Callback _internalCallback;
        private readonly GirModel.Callback _callback;
        private Model.Public.ReturnType? _publicReturnType;
        private Model.Internal.ReturnType? _internalReturnType;
        private IEnumerable<Model.Public.Parameter>? _publicParameters;
        private IEnumerable<Model.Internal.Parameter>? _internalParameters;

        public string Name => _callback.Name + "Handler";
        public string DelegateType => _callback.Name;
        public string InternalDelegateType => _callback.Namespace.GetInternalName() + "." + _callback.Name;
        
        public string NamespaceName => _callback.Namespace.Name;

        public Model.Public.ReturnType PublicReturnType => _publicReturnType ??= _callback.ReturnType.CreatePublicModel();
        public Model.Internal.ReturnType InternalReturnType => _internalReturnType ??= _internalCallback.ReturnType;
        public IEnumerable<Model.Internal.Parameter> InternalParameters => _internalParameters ??= _callback.Parameters.CreateInternalModelsForCallback();
        public IEnumerable<Model.Public.Parameter> PublicParameters => _publicParameters ??= _callback.Parameters.CreatePublicModels();
        
        public PublicHandlerModel(GirModel.Callback callback)
        {
            _callback = callback;
            _internalCallback = new Model.Internal.Callback(callback);
        }
    }
}
