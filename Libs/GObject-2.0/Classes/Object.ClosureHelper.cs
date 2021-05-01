using System;
using System.Diagnostics;

namespace GObject
{
    public partial class Object
    {
        protected internal delegate void ActionRefValues(ref Native.Value.Struct[] items);

        private class ClosureHelper : IDisposable
        {
            private readonly ActionRefValues _callback;
            private readonly Closure _closure;

            public Native.Closure.Handle? Handle => _closure.Handle;

            public ClosureHelper(ActionRefValues action)
            {
                //TODO Use MarshalCallback
                _closure = new Closure(Workaround);
                _callback = action;
            }


            //TODO: Delete this method
            private void Workaround()
            {
                var data = Array.Empty<Native.Value.Struct>();
                _callback(ref data);
            }

            public void MarshalCallback(Closure closure, Value? returnValue, uint nParamValues, Value[] paramValues, IntPtr? invocationHint, IntPtr? marshalData)
            {
                Debug.Assert(
                    condition: paramValues.Length == nParamValues,
                    message: "Values were not marshalled correctly. Breakage may occur"
                );

                //TODO forward values

                var data = Array.Empty<Native.Value.Struct>();
                _callback(ref data);
            }

            public void Dispose()
            {
                _closure.Dispose();
            }
        }
    }
}
