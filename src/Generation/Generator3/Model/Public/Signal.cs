using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Public
{
    public class Signal
    {
        private readonly GirModel.Signal _signal;

        public string ManagedName => "On" + _signal.ManagedName;
        public string NativeName => _signal.NativeName;
        public string DescriptorName => _signal.ManagedName + "Signal";
        public string? ArgsName => _signal.ManagedName + "SignalArgs";
        public string ClassName { get; }
        public bool HasArgs => _signal.Parameters.Any();
        public string GenericArgs =>  HasArgs 
            ? $"{ClassName}, {ArgsName}"
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
