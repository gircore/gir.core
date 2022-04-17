using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using GirModel;

namespace Generator3.Generation.Callback
{
    public abstract class CommonHandlerModel
    {
        private readonly Model.Internal.Callback _internalCallback;
        private readonly GirModel.Callback _callback;
        private Model.Internal.ReturnType? _internalReturnType;
        private IEnumerable<Parameter>? _internalParameters;
        private IEnumerable<Parameter>? _publicParameters;

        public string Name { get; }
        public string DelegateType => _callback.Name;
        public string InternalDelegateType => _callback.Namespace.GetInternalName() + "." + _callback.Name;
        public string NamespaceName => _callback.Namespace.GetPublicName();
        public PlatformDependent? PlatformDependent => _callback as PlatformDependent;
        public Model.Internal.ReturnType InternalReturnType
            => _internalReturnType ??= _internalCallback.ReturnType;
        public IEnumerable<Parameter> InternalParameters
            => _internalParameters ??= _callback.Parameters;
        public IEnumerable<Parameter> PublicParameters
            => _publicParameters ??= _callback.Parameters.Where(x => x.Closure is null or 0);

        protected CommonHandlerModel(GirModel.Callback callback, string suffix)
        {
            _callback = callback;
            _internalCallback = new Model.Internal.Callback(callback);

            // Let derived classes add a suffix (e.g. 'AsyncHandler')
            Name = callback.Name + suffix;
        }
    }
}
