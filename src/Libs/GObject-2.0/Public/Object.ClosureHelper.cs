using System;
using System.Diagnostics;

namespace GObject;

public partial class Object
{
    protected internal delegate void ActionRefValues(ref Value[] items);

    private class ClosureHelper : IDisposable
    {
        private readonly ClosureMarshal? _marshalCallback;
        private readonly ActionRefValues _callback;
        private readonly Closure _closure;

        public Internal.ClosureHandle? Handle => _closure.Handle;

        public ClosureHelper(ActionRefValues action)
        {
            _marshalCallback = MarshalCallback;
            _closure = new Closure(_marshalCallback);
            _callback = action;
        }

        //From this method the "IntPtr? userData" parameter was removed due to https://gitlab.gnome.org/GNOME/glib/-/issues/2827
        //If this issue is fixed it must be added again
        private void MarshalCallback(Closure closure, Value? returnValue, uint nParamValues, Value[] paramValues, IntPtr? invocationHint)
        {
            Debug.Assert(
                condition: paramValues.Length == nParamValues,
                message: "Values were not marshalled correctly. Breakage may occur"
            );

            _callback(ref paramValues);
        }

        public void Dispose()
        {
            _closure.Dispose();
        }
    }
}
