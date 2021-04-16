using System;
using System.Diagnostics;

namespace GObject
{
    public partial class Object
    {
        protected internal delegate void ActionRefValues(ref Native.Value.Struct[] items);

        private class ClosureHelper : IDisposable
        {
            private readonly ActionRefValues? _callback;
            private readonly Closure _closure;

            public Native.Closure.Handle? Handle => _closure.Handle;

            public ClosureHelper(ActionRefValues action)
            {
                _closure = new Closure(MarshalCallback);
                _callback = action;
            }

            //private void MarshalCallback(Native.Closure.Struct closure, Native.Value.Struct? returnvalue, uint nparamvalues, Native.Value.Struct[] paramvalues, IntPtr? invocationhint, IntPtr? marshaldata)
            public void MarshalCallback(Closure closure, Value? returnValue, uint nParamValues, Value[] paramValues, IntPtr? invocationHint, IntPtr? marshalData)
            {
                Debug.Assert(
                    condition: paramValues.Length == nParamValues,
                    message: "Values were not marshalled correctly. Breakage may occur"
                );

                // TODO: ClosureHelper needs rewriting
                // _callback?.Invoke(ref paramValues);
            }

            public void Dispose()
            {
                _closure.Dispose();
            }
        }
    }
}
