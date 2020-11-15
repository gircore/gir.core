using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GObject
{
    public delegate void ActionRefValues(ref Value[] items);

    internal class ClosureHelper : IDisposable
    {
        #region Fields

        private static readonly Dictionary<Delegate, ClosureHelper> Handlers = new Dictionary<Delegate, ClosureHelper>();

        private bool _disposedValue;
        private readonly Action? _callback;
        private readonly ActionRefValues? _callbackRefValues;

        #endregion

        #region Properties

        internal IntPtr Handle { get; private set; }

        #endregion

        #region Constructors

        public ClosureHelper(Object obj, Action callback) : this(obj)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            Handlers[callback] = this;
        }

        public ClosureHelper(Object obj, ActionRefValues callbackRefValues) : this(obj)
        {
            _callbackRefValues = callbackRefValues ?? throw new ArgumentNullException(nameof(callbackRefValues));
            Handlers[callbackRefValues] = this;
        }

        private ClosureHelper(Object obj)
        {
            Handle = Closure.Native.new_object((uint) Marshal.SizeOf(typeof(Closure)), obj.Handle);
            Closure.Native.set_marshal(Handle, MarshalCallback);
        }

        ~ClosureHelper() => Dispose(false);

        #endregion

        #region Methods

        private void MarshalCallback(IntPtr closure, ref Value return_value, uint n_param_values,
            Value[] param_values, IntPtr invocation_hint, IntPtr marshal_data)
        {
            _callback?.Invoke();

            _callbackRefValues?.Invoke(ref param_values);
        }

        public static bool TryGetByDelegate(Action action, [MaybeNullWhen(false)] out ClosureHelper closure)
        {
            return Handlers.TryGetValue(action, out closure);
        }

        public static bool TryGetByDelegate(ActionRefValues action, [MaybeNullWhen(false)] out ClosureHelper closure)
        {
            return Handlers.TryGetValue(action, out closure);
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                Closure.Native.unref(Handle);
                Handle = IntPtr.Zero;
                _disposedValue = true;
            }
        }

        #endregion
    }
}
