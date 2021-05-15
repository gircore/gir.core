using System.Linq;
using Gir.Model;

namespace Generator.Services.Writer
{
    internal class SignalHelper
    {
        private readonly Signal _signal;

        public bool NeedsArgs => _signal.ParameterList.Any();
        public string EventName => $"On{_signal.SymbolName}";
        public string EventDescriptor => $"{_signal.SymbolName}Signal";
        public string ArgsType => _signal.SymbolName + "SignalArgs";

        public SignalHelper(Signal signal)
        {
            _signal = signal;
        }
    }
}
