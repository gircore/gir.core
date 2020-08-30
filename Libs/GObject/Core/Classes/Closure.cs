using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GObject
{
    public delegate void ActionRefValues(ref Sys.Value[] items); //Todo: This exposes the sys namespace

    internal partial class Closure
    {
        #region Fields

        private readonly Action? callback;
        private readonly ActionRefValues? callbackRefValues;
        private static readonly Dictionary<Delegate, Closure> handlers = new Dictionary<Delegate, Closure>();

        #endregion

        #region Properties

        internal IntPtr Handle { get; private set; }

        #endregion

        #region Constructors

        public Closure(Object obj, Action callback) : this(obj)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
            handlers[callback] = this;
        }

        public Closure(Object obj, ActionRefValues callbackRefValues) : this(obj)
        {
            this.callbackRefValues = callbackRefValues ?? throw new ArgumentNullException(nameof(callbackRefValues));
            handlers[callbackRefValues] = this;
        }

        private Closure(Object obj)
        {
            Handle = Sys.Closure.new_object((uint) Marshal.SizeOf(typeof(Sys.Closure)), obj.Handle);
            Sys.Closure.set_marshal(Handle, MarshalCallback);
        }

        #endregion

        #region Methods
        private void MarshalCallback(IntPtr closure, ref Sys.Value return_value, uint n_param_values,
            Sys.Value[] param_values, IntPtr invocation_hint, IntPtr marshal_data)
        {
            callback?.Invoke();

            callbackRefValues?.Invoke(ref param_values);
        }

        public static bool TryGetByDelegate(Action action, out Closure closure)
        {
            return handlers.TryGetValue(action, out closure);
        }

        public static bool TryGetByDelegate(ActionRefValues action, out Closure closure)
        {
            return handlers.TryGetValue(action, out closure);
        }
        #endregion
    }
}