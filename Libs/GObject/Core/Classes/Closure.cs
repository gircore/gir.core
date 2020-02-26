using System;
using System.Runtime.InteropServices;

namespace GObject.Core
{
    public delegate void ActionRefValues(ref Value[] items);

    public partial class GClosure
    {
        private IntPtr handle;
        private readonly Action? callback;
        private ActionRefValues? callbackRefValues;

        public GClosure(GObject obj, Action callback) : this(obj)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public GClosure(GObject obj, ActionRefValues callbackRefValues) : this(obj)
        {
            this.callbackRefValues = callbackRefValues ?? throw new ArgumentNullException(nameof(callbackRefValues));
        }

        private GClosure(GObject obj)
        {
            handle = Closure.new_object((uint)Marshal.SizeOf(typeof(global::GObject.Closure)), obj);
            Closure.set_marshal(handle, MarshalCallback);
        }

        private void MarshalCallback (IntPtr closure, ref global::GObject.Value return_value, uint n_param_values, global::GObject.Value[] param_values, IntPtr invocation_hint, IntPtr marshal_data)
        {
            if(callback is {})
                callback();
            
            if(callbackRefValues is {})
                callbackRefValues(ref param_values);
        }

        public static implicit operator IntPtr (GClosure closure) => closure.handle;
    }
}