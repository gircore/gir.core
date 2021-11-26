using System.Collections.Generic;

namespace Generator3.Model.Internal
{
    public class Method
    {
        private InstanceParameter? _instanceParameter;
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;

        public GirModel.Method Model { get; }

        public string Name => Model.Name;
        public ReturnType ReturnType => _returnType ??= Model.ReturnType.CreateInternalModel();
        public string CIdentifier => Model.CIdentifier;

        public InstanceParameter InstanceParameter => _instanceParameter ??= Model.InstanceParameter.CreateInternalModel();
        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.CreateInternalModelsForMethod();

        public string NamespaceName { get; }

        public Method(GirModel.Method method, string namespaceName)
        {
            Model = method;
            NamespaceName = namespaceName;
        }
    }
}
