using System.Collections.Generic;

namespace Generator3.Model.Internal
{
    public class Function
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;

        public GirModel.Function Model { get; }

        public string Name => Model.Name;
        public ReturnType ReturnType => _returnType ??= Model.ReturnType.CreateInternalModel();
        public string CIdentifier => Model.CIdentifier;
        public string NameSpaceName => Model.Namespace.Name;

        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.CreateInternalModels();

        public Function(GirModel.Function function)
        {
            Model = function;
        }
    }
}
