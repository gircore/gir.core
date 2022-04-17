using System.Collections.Generic;
using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class Constructor
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;

        public GirModel.Constructor Model { get; }

        public string Name => Model.GetInternalName();
        public ReturnType ReturnType => _returnType ??= Model.ReturnType.CreateInternalModel();
        public string CIdentifier => Model.CIdentifier;

        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.CreateInternalModels();

        public Constructor(GirModel.Constructor constructor)
        {
            Model = constructor;
        }
    }
}
