using System.Collections.Generic;

namespace Generator3.Model.Internal
{
    public class Callback
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;
        
        public GirModel.Callback Model { get; }

        public string Name => Model.Name + "Callback";
        public ReturnType ReturnType => _returnType ??= Model.ReturnType.CreateInternalModel();
        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.CreateInternalModels();
        
        public Callback(GirModel.Callback model)
        {
            Model = model;
        }
    }
}
