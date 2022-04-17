using System.Collections.Generic;
using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class Function
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;

        public GirModel.Function Model { get; }

        public string Name => Model.GetInternalName();
        public ReturnType ReturnType => _returnType ??= Model.ReturnType.CreateInternalModel();
        public string CIdentifier => Model.CIdentifier;
        public string NameSpaceName => Model.Namespace.Name;
        public GirModel.PlatformDependent? PlatformDependent => Model as GirModel.PlatformDependent;

        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.CreateInternalModels();

        public Function(GirModel.Function function)
        {
            Model = function;
        }
    }
}
