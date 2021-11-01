using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class NativeCallback
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;
        
        public GirModel.Callback Model { get; }

        public string Name => Model.Name + "Callback";
        public ReturnType ReturnType => _returnType ??= ReturnType.CreateNative(Model.ReturnType);
        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.Select(Parameter.CreateForNative);
        
        public NativeCallback(GirModel.Callback model)
        {
            Model = model;
        }
    }
}
