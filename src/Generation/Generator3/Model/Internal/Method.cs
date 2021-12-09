using System.Collections.Generic;
using Generator3.Renderer.Converter;

namespace Generator3.Model.Internal
{
    public class Method
    {
        private InstanceParameter? _instanceParameter;
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;
        private string? _name;

        public GirModel.Method Model { get; }

        public string Name => _name ??= Model.GetInternalName();
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
