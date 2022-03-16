using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Public;

namespace Generator3.Generation.Callback
{
    public class PublicDelegateModel
    {
        private readonly GirModel.Callback _callback;
        private ReturnType? _returnType;
        private IEnumerable<Parameter>? _parameters;

        public string Name => _callback.Name;
        public string NamespaceName => _callback.Namespace.GetPublicName();

        public ReturnType ReturnType => _returnType ??= _callback.ReturnType.CreatePublicModel();
        public IEnumerable<Parameter> Parameters
            => _parameters ??= _callback.Parameters
                .Where(p => p.Closure is null or 0)
                .CreatePublicModels()
                .ToList();

        public PublicDelegateModel(GirModel.Callback callback)
        {
            _callback = callback;
        }
    }
}
