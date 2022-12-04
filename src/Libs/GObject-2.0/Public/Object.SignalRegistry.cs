using System;
using System.Collections.Generic;

namespace GObject;

public partial class Object
{
    internal sealed class SignalRegistry : IDisposable
    {
        private readonly Object _object;
        private readonly Dictionary<string, ClosureRegistry> _closureRegistries = new();

        internal SignalRegistry(Object @object)
        {
            _object = @object;
        }

        public ClosureRegistry GetClosureRegistry(string signalName)
        {
            if (_closureRegistries.TryGetValue(signalName, out var closureRegistry))
                return closureRegistry;

            var newClosureRegistry = new ClosureRegistry(_object, signalName);
            _closureRegistries[signalName] = newClosureRegistry;

            return newClosureRegistry;
        }

        public void Dispose()
        {
            foreach (var closureRegistry in _closureRegistries.Values)
                closureRegistry.Dispose();
        }
    }
}
