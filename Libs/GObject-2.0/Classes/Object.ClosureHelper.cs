using System;
using System.Diagnostics;

namespace GObject
{
    public partial class Object
    {
        protected internal delegate void ActionRefValues(ref Value.Native.Struct[] items);

        private class ClosureHelper : IDisposable
        {
            private readonly ActionRefValues? _callback;
            private readonly Closure _closure;

            public IntPtr Handle => _closure.Handle;

            public ClosureHelper(ActionRefValues action)
            {
                _closure = new Closure(MarshalCallback);
                _callback = action;
            }

            private void MarshalCallback(Closure.Native.Struct closure, Value.Native.Struct? returnvalue, uint nparamvalues, Value.Native.Struct[] paramvalues, IntPtr? invocationhint, IntPtr? marshaldata)
            {
                Debug.Assert(
                    condition: paramvalues.Length == nparamvalues,
                    message: "Values were not marshalled correctly. Breakage may occur"
                );

                _callback?.Invoke(ref paramvalues);
            }

            public void Dispose()
            {
                _closure.Dispose();
            }
        }
    }
}
