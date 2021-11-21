using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Public;

namespace Generator3.Generation.Callback
{
    public class PublicNotifiedHandlerModel
    {
        private readonly GirModel.Callback _callback;
        private ReturnType? _returnType;
        private IEnumerable<StandardParameter>? _parameters;

        public string Name => _callback.Name + "NotifiedHandler";
        public string DelegateType => _callback.Name;
        public string InternalDelegateType => _callback.Namespace.GetInternalName() + "." + _callback.Name;
        
        public string NamespaceName => _callback.Namespace.Name;

        public ReturnType ReturnType => _returnType ??= _callback.ReturnType.CreatePublicModel();
        public IEnumerable<StandardParameter> Parameters => _parameters ??= _callback.Parameters.Select(x => new StandardParameter(x));
        
        public PublicNotifiedHandlerModel(GirModel.Callback callback)
        {
            _callback = callback;
        }
    }
}
