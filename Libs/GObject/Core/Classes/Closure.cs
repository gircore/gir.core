using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public delegate void ActionRefValues(ref Sys.Value[] items);

    public partial class Closure
    {
        private IntPtr handle;
        public IntPtr Handle => handle;
        
        private readonly Action? callback;
        private readonly ActionRefValues? callbackRefValues;

        public Closure(Object obj, Action callback) : this(obj)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public Closure(Object obj, ActionRefValues callbackRefValues) : this(obj)
        {
            this.callbackRefValues = callbackRefValues ?? throw new ArgumentNullException(nameof(callbackRefValues));
        }

        private Closure(Object obj)
        {
            handle = Sys.Closure.new_object((uint)Marshal.SizeOf(typeof(Sys.Closure)), obj.Handle);
            Sys.Closure.set_marshal(handle, MarshalCallback);
        }

        private void MarshalCallback (IntPtr closure, ref Sys.Value return_value, uint n_param_values, Sys.Value[] param_values, IntPtr invocation_hint, IntPtr marshal_data)
        {
            callback?.Invoke();

            callbackRefValues?.Invoke(ref param_values);
        }
    }
}