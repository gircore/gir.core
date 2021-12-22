using System.Collections.Generic;
using Generator3.Converter;
using Generator3.Model.Internal;

namespace Generator3.Generation.Callback
{
    public class InternalDelegateModel
    {
        private readonly GirModel.Callback _callback;
        private ReturnType? _returnType;
        private IEnumerable<Parameter>? _parameters;

        public string Name => _callback.Name;
        public string NamespaceName => _callback.Namespace.GetInternalName();

        public ReturnType ReturnType => _returnType ??= _callback.ReturnType.CreateInternalModel();
        public IEnumerable<Parameter> Parameters => _parameters ??= _callback.Parameters.CreateInternalModelsForCallback();

        public InternalDelegateModel(GirModel.Callback callback)
        {
            _callback = callback;
        }
    }
}
