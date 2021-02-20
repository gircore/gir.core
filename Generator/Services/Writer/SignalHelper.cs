using System.Linq;
using Repository.Model;

namespace Generator.Services.Writer
{
    internal class SignalHelper
    {
        private readonly Signal _signal;

        public bool NeedsArgs => _signal.Arguments.Any();
        public string EventName => $"On{_signal.ManagedName}";
        public string EventDescriptor => $"{_signal.ManagedName}Signal";
        public string ArgsType => _signal.ManagedName + "SignalArgs";

        public SignalHelper(Signal signal)
        {
            _signal = signal;
        }
    }
}
