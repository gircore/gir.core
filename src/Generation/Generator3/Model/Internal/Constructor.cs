using System.Collections.Generic;

namespace Generator3.Model.Internal
{
    public class Constructor
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;

        public GirModel.Method Model { get; }

        public string Name => Model.Name;
        public ReturnType ReturnType => _returnType ??= Model.ReturnType.CreateInternalModel();
        public string CIdentifier => Model.CIdentifier;
        public string NameSpaceName => Model.Namespace.Name;

        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.CreateInternalModels();

        public Constructor(GirModel.Method constructor)
        {
            Model = constructor;
        }
    }
}
