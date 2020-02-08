using System;
using System.Runtime.InteropServices;

namespace GObject.Core
{
    public partial class GClosure
    {
        private IntPtr handle;
        private readonly Action callback;

        public GClosure(GObject obj, Action callback)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));

            handle = Closure.new_object((uint)Marshal.SizeOf(typeof(global::GObject.Closure)), obj);
            Closure.set_marshal(handle, MarshalCallback);
        }

        private void MarshalCallback (IntPtr closure, ref global::GObject.Value return_value, uint n_param_values, global::GObject.Value[] param_values, IntPtr invocation_hint, IntPtr marshal_data)
        {
            callback();
        }

        public static implicit operator IntPtr (GClosure closure) => closure.handle;
    }
}