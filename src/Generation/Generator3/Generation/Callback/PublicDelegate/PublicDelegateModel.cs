using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Public;

namespace Generator3.Generation.Callback
{
    public class PublicDelegateModel
    {
        private readonly GirModel.Callback _callback;
        private ReturnType? _returnType;
        private IEnumerable<StandardParameter>? _parameters;

        public string Name => _callback.Name;
        public string NamespaceName => _callback.Namespace.Name;

        public ReturnType ReturnType => _returnType ??= _callback.ReturnType.CreatePublicModel();
        public IEnumerable<StandardParameter> Parameters => _parameters ??= _callback.Parameters.Select(x => new StandardParameter(x));
        
        public PublicDelegateModel(GirModel.Callback callback)
        {
            _callback = callback;
        }
    }
}
