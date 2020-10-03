using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GObject
{
    public delegate void ActionRefValues(ref Value[] items);

    internal class ClosureHelper : IDisposable
    {
        #region Fields

        private static readonly Dictionary<Delegate, ClosureHelper> handlers = new Dictionary<Delegate, ClosureHelper>();
        
        private bool disposedValue = false;
        private readonly Action? callback;
        private readonly ActionRefValues? callbackRefValues;

        #endregion

        #region Properties

        internal IntPtr Handle { get; private set; }

        #endregion

        #region Constructors

        public ClosureHelper(Object obj, Action callback) : this(obj)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
            handlers[callback] = this;
        }

        public ClosureHelper(Object obj, ActionRefValues callbackRefValues) : this(obj)
        {
            this.callbackRefValues = callbackRefValues ?? throw new ArgumentNullException(nameof(callbackRefValues));
            handlers[callbackRefValues] = this;
        }

        private ClosureHelper(Object obj)
        {
            Handle = Closure.new_object((uint) Marshal.SizeOf(typeof(Closure)), obj.Handle);
            Closure.set_marshal(Handle, MarshalCallback);
        }
        
        ~ClosureHelper() => Dispose(false);

        #endregion

        #region Methods
        private void MarshalCallback(IntPtr closure, ref Value return_value, uint n_param_values,
            Value[] param_values, IntPtr invocation_hint, IntPtr marshal_data)
        {
            callback?.Invoke();

            callbackRefValues?.Invoke(ref param_values);
        }

        public static bool TryGetByDelegate(Action action, out ClosureHelper closure)
        {
            return handlers.TryGetValue(action, out closure);
        }

        public static bool TryGetByDelegate(ActionRefValues action, out ClosureHelper closure)
        {
            return handlers.TryGetValue(action, out closure);
        }
        #endregion
        
        #region IDisposeable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Closure.unref(Handle);
                Handle = IntPtr.Zero;
                disposedValue = true;
            }
        }
        #endregion
    }
}