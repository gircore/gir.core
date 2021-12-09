using System.Collections.Generic;
using System.Linq;
using Generator3.Renderer.Converter;

namespace Generator3.Model.Public
{
    public class Signal
    {
        private readonly GirModel.Signal _signal;

        public string PublicName => _signal.GetPublicName();
        public string NativeName => _signal.Name;
        public string DescriptorName => _signal.GetDescriptorName();
        public string ArgsClassName => _signal.GetArgsClassName();
        public string ClassName { get; }
        public bool HasArgs => _signal.Parameters.Any();
        public string GenericArgs => HasArgs 
            ? $"{ClassName}, {ArgsClassName}"
            : ClassName;
        public IEnumerable<Parameter> Parameters { get; }
        
        public Signal(GirModel.Signal signal, string className)
        {
            _signal = signal;
            ClassName = className;
            Parameters = _signal.Parameters.CreatePublicModels();
        }
    }
}
