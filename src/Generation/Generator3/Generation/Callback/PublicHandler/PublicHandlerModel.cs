using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Internal;
using Generator3.Model.Public;

namespace Generator3.Generation.Callback
{
    public class PublicHandlerModel
    {
        private readonly Model.Internal.Callback _internalCallback;
        private readonly GirModel.Callback _callback;
        private Model.Internal.ReturnType? _internalReturnType;
        private IEnumerable<GirModel.Parameter>? _internalParameters;
        private IEnumerable<GirModel.Parameter>? _publicParameters;

        public string Name => _callback.Name + "Handler";
        public string DelegateType => _callback.Name;
        public string InternalDelegateType => _callback.Namespace.GetInternalName() + "." + _callback.Name;

        public string NamespaceName => _callback.Namespace.Name;

        public Model.Internal.ReturnType InternalReturnType
            => _internalReturnType ??= _internalCallback.ReturnType;

        public IEnumerable<GirModel.Parameter> InternalParameters
            => _internalParameters ??= _callback.Parameters;

        /// <remark>
        /// Excludes user data parameters, they are not part of the public API
        /// </remark>
        public IEnumerable<GirModel.Parameter> PublicParameters
            => _publicParameters ??= _callback.Parameters.Where(x => x.Closure is null or 0);

        public PublicHandlerModel(GirModel.Callback callback)
        {
            _callback = callback;
            _internalCallback = new Model.Internal.Callback(callback);
        }
    }
}
